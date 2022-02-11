using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class PutShoppingListEndpoint : BaseEndpoint
{
    private readonly ILogger<PutShoppingListEndpoint> _logger;
    private readonly ICurrentUserProvider _currentUserService;
    private readonly IUpdateShoppingListCommand _updateShoppingListCommand;
    private readonly IValidateShoppingListPermissionQuery _validateShoppingListPermissionQuery;
    private readonly IGetOneUserQuery _getOneUserQuery;

    public PutShoppingListEndpoint(
        ILogger<PutShoppingListEndpoint> logger,
        ICurrentUserProvider currentUserService,
        IValidateShoppingListPermissionQuery validateShoppingListPermissionQuery,
        IGetOneUserQuery getOneUserQuery,
        IUpdateShoppingListCommand updateShoppingListCommand)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _updateShoppingListCommand = updateShoppingListCommand;
        _validateShoppingListPermissionQuery = validateShoppingListPermissionQuery;
        _getOneUserQuery = getOneUserQuery;
    }

    [Authorize("shop.api.access")]
    [HttpPut("/api/v1/lists/{guid}")]
    public async Task<ActionResult<Response>> HandleAsync(Guid guid, [FromBody] ShoppingListDto shoppingListDto, CancellationToken ct = new())
    {
        try
        {
            var currentUser = _currentUserService.CurrentUser;
            if (currentUser == null) return Unauthorized();

            var validation = await ValidateRequest(guid, shoppingListDto, ct);
            if (!validation.IsSuccess) return BadRequest(new Response(validation.Messages));

            var permission = await _validateShoppingListPermissionQuery.ExecuteAsync(
                currentUser.Guid,
                shoppingListDto.Guid,
                ShoppingListPermission.Administrator.Name,
                ct);
            if (!permission.IsSuccess) return Forbid();

            var userListPermissions = shoppingListDto.Permissions.Select(p =>
            {
                var user = _getOneUserQuery.ExecuteAsync(p.User.Guid, ct).GetAwaiter().GetResult();
                return new UserListPermissionModel
                {
                    User = user,
                    Permission = p.Permission
                };
            });
            var shoppingListModel = new ShoppingListModel
            {
                Guid = shoppingListDto.Guid,
                Name = shoppingListDto.Name,
                Permissions = userListPermissions
            };
            var result = await _updateShoppingListCommand.ExecuteAsync(shoppingListModel, ct);
            if (!result.IsSuccess) return InternalServerError();

            return Ok(new Response());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return InternalServerError();
        }
    }

    private async Task<Response> ValidateRequest(Guid guid, ShoppingListDto dto, CancellationToken ct = new())
    {
        return await Task.Run(() =>
        {
            if (guid == Guid.Empty)
                return new Response("Invalid shopping list GUID was provided in URI.");

            if (dto == null)
                return new Response("Shopping List data was malformed in request body.");

            if (guid != dto.Guid)
                return new Response("GUID from the URI does not match the GUID from the request body.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return new Response($"Shopping List {nameof(dto.Name)} is missing.");

            if (dto.Permissions.Count(p => p.Permission.ToUpper() == ShoppingListPermission.Owner.ToString()) != 1)
                return new Response($"Permissions must include one Owner.");

            return new Response();
        }, ct);
    }
}