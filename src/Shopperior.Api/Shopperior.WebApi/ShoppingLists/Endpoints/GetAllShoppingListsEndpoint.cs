using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class GetAllShoppingListsEndpoint : BaseEndpoint
{
    private readonly ILogger<GetAllShoppingListsEndpoint> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IGetAllShoppingListsQuery _getAllShoppingListsQuery;

    public GetAllShoppingListsEndpoint(
        ILogger<GetAllShoppingListsEndpoint> logger,
        ICurrentUserService currentUserService,
        IGetAllShoppingListsQuery getAllShoppingListsQuery)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _getAllShoppingListsQuery = getAllShoppingListsQuery;
    }

    [Authorize("shop.api.access")]
    [HttpGet("api/v1/lists")]
    public async Task<ActionResult<Response<ShoppingListDto[]>>> HandleAsync(CancellationToken cancellationToken = new())
    {
        try
        {
            var currentUser = _currentUserService.CurrentUser;
            if (currentUser == null) return Unauthorized();

            var lists = await _getAllShoppingListsQuery.ExecuteAsync(currentUser.Username, cancellationToken);
            var models = lists.Select(l => new ShoppingListDto
            {
                Guid = l.Guid,
                Name = l.Name
            });
            return Ok(new Response<ShoppingListDto[]>(models.ToArray()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return StatusCode(500);
        }
    }
}