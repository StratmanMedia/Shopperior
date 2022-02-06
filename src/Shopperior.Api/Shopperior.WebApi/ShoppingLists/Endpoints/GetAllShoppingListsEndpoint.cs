using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class GetAllShoppingListsEndpoint : BaseEndpoint
{
    private readonly ILogger<GetAllShoppingListsEndpoint> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IGetAllShoppingListsQuery _getAllShoppingListsQuery;
    private readonly IListItemDtoResolver _listItemDtoResolver;

    public GetAllShoppingListsEndpoint(
        ILogger<GetAllShoppingListsEndpoint> logger,
        ICurrentUserService currentUserService,
        IGetAllShoppingListsQuery getAllShoppingListsQuery,
        IListItemDtoResolver listItemDtoResolver)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _getAllShoppingListsQuery = getAllShoppingListsQuery;
        _listItemDtoResolver = listItemDtoResolver;
    }

    [Authorize("shop.api.access")]
    [HttpGet("api/v1/lists")]
    public async Task<ActionResult<Response<ShoppingListDto[]>>> HandleAsync(CancellationToken ct = new CancellationToken())
    {
        try
        {
            var currentUser = _currentUserService.CurrentUser;
            if (currentUser == null) return Unauthorized();

            var lists = await _getAllShoppingListsQuery.ExecuteAsync(currentUser.Username, ct);

            var models = new List<ShoppingListDto>();
            foreach(var l in lists)
            {
                var permissions = l.Permissions.Select(p =>
                {
                    var userDto = new UserDto
                    {
                        Guid = p.User.Guid,
                        Username = p.User.Username,
                        EmailAddress = p.User.EmailAddress,
                        FirstName = p.User.FirstName,
                        LastName = p.User.LastName
                    };
                    return new UserListPermissionDto
                    {
                        User = userDto,
                        ShoppingListGuid = p.ShoppingListGuid,
                        Permission = p.Permission
                    };
                });
                var items = new List<ListItemDto>();
                foreach (var i in l.Items)
                {
                    var item = await _listItemDtoResolver.ResolveAsync(i);
                    items.Add(item);
                }
                var list = new ShoppingListDto
                {
                    Guid = l.Guid,
                    Name = l.Name,
                    Permissions = permissions,
                    Items = items
                };
                models.Add(list);
            }

            return Ok(new Response<ShoppingListDto[]>(models.ToArray()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return StatusCode(500);
        }
    }
}