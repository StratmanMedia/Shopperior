using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class DeleteShoppingListEndpoint : BaseEndpoint<DeleteShoppingListEndpoint>
{
    private readonly ILogger<DeleteShoppingListEndpoint> _logger;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IValidateShoppingListPermissionQuery _validateShoppingListPermissionQuery;
    private readonly IDeleteShoppingListCommand _deleteShoppingListCommand;

    public DeleteShoppingListEndpoint(
        ILogger<DeleteShoppingListEndpoint> logger,
        ICurrentUserProvider currentUserProvider,
        IValidateShoppingListPermissionQuery validateShoppingListPermissionQuery,
        IDeleteShoppingListCommand deleteShoppingListCommand)
    : base(logger)
    {
        _logger = logger;
        _currentUserProvider = currentUserProvider;
        _validateShoppingListPermissionQuery = validateShoppingListPermissionQuery;
        _deleteShoppingListCommand = deleteShoppingListCommand;
    }

    [Authorize("shop.api.access")]
    [HttpDelete("/api/v1/lists/{guid}")]
    public async Task<ActionResult<Response>> HandleAsync(Guid guid, CancellationToken ct = new CancellationToken())
    {
        return await TryActionAsync(() => EndpointAction(guid, ct));
    }

    private async Task EndpointAction(Guid guid, CancellationToken ct = new CancellationToken())
    {
        await ValidateRequest(guid);

        var currentUser = _currentUserProvider.CurrentUser;
        if (currentUser == null)
            throw new UnauthorizedAccessException();

        var permission = await _validateShoppingListPermissionQuery.ExecuteAsync(
            currentUser.Guid,
            guid,
            ShoppingListPermission.Administrator.ToString(),
            ct);
        if (!permission.IsSuccess)
            throw new UnauthorizedAccessException();

        var result = await _deleteShoppingListCommand.ExecuteAsync(new DeleteShoppingListRequest
        {
            UserGuid = currentUser.Guid,
            ShoppingListGuid = guid
        }, ct);
        if (!result.IsSuccess) throw new InvalidOperationException(string.Join('|', result.Messages));
    }

    private Task ValidateRequest(Guid request)
    {
        if (request == Guid.Empty)
            throw new BadHttpRequestException("Invalid list GUID was provided.");

        return Task.CompletedTask;
    }
}