using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using Shopperior.WebApi.Users.Interfaces;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class GetAllShoppingListsEndpoint : BaseEndpoint<GetAllShoppingListsEndpoint>
{
    private readonly ILogger<GetAllShoppingListsEndpoint> _logger;
    private readonly ICurrentUserProvider _currentUserService;
    private readonly IGetAllShoppingListsQuery _getAllShoppingListsQuery;
    private readonly IShoppingListDtoResolver _shoppingListDtoResolver;

    public GetAllShoppingListsEndpoint(
        ILogger<GetAllShoppingListsEndpoint> logger,
        ICurrentUserProvider currentUserService,
        IGetAllShoppingListsQuery getAllShoppingListsQuery,
        IShoppingListDtoResolver shoppingListDtoResolver)
    : base(logger)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _getAllShoppingListsQuery = getAllShoppingListsQuery;
        _shoppingListDtoResolver = shoppingListDtoResolver;
    }

    [Authorize("shop.api.access")]
    [HttpGet("api/v1/lists")]
    public async Task<ActionResult<Response<ShoppingListDto[]>>> HandleAsync(CancellationToken ct = default)
    {
        return await TryActionAsync(() => EndpointAction(ct));
    }

    private async Task<ShoppingListDto[]> EndpointAction(CancellationToken ct = default)
    {
        var currentUser = _currentUserService.CurrentUser;
        if (currentUser == null)
            throw new UnauthorizedAccessException();

        var listModels = await _getAllShoppingListsQuery.ExecuteAsync(currentUser.Guid, ct);

        var lists = new List<ShoppingListDto>();
        foreach (var l in listModels)
        {
            lists.Add(await _shoppingListDtoResolver.ResolveAsync(l, ct));
        }

        return lists.ToArray();
    }
}