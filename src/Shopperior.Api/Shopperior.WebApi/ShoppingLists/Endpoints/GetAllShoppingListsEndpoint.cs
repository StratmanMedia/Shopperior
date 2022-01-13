using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.Users.Resolvers;
using StratmanMedia.Auth;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class GetAllShoppingListsEndpoint : BaseEndpoint
{
    private readonly ILogger<GetAllShoppingListsEndpoint> _logger;
    private readonly IGetAllShoppingListsQuery _getAllShoppingListsQuery;
    private readonly ICurrentUserResolver _currentUserResolver;

    public GetAllShoppingListsEndpoint(
        ILogger<GetAllShoppingListsEndpoint> logger,
        IGetAllShoppingListsQuery getAllShoppingListsQuery,
        ICurrentUserResolver currentUserResolver)
    {
        _logger = logger;
        _getAllShoppingListsQuery = getAllShoppingListsQuery;
        _currentUserResolver = currentUserResolver;
    }

    [Authorize("shop.api.access")]
    [HttpGet("api/v1/lists")]
    public async Task<ActionResult<Response<ShoppingListDto[]>>> HandleAsync(CancellationToken cancellationToken = new())
    {
        var currentUser = await _currentUserResolver.Resolve(User, GetAuthorizationHeaderValue(), cancellationToken);

        try
        {
            var lists = await _getAllShoppingListsQuery.ExecuteAsync(cancellationToken);
            var models = lists.Select(l => new ShoppingListDto
            {
                Guid = l.Guid,
                Name = l.Name
            });
            return Ok(new Response<ShoppingListDto[]>(models.ToArray()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.JoinAllMessages());
            return StatusCode(500);
        }
    }
}