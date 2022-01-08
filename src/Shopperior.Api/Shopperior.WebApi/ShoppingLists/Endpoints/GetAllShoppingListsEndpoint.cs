using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.ShoppingLists.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class GetAllShoppingListsEndpoint : BaseEndpoint
{
    private readonly ILogger<GetAllShoppingListsEndpoint> _logger;
    private readonly IGetAllShoppingListsQuery _getAllShoppingListsQuery;

    public GetAllShoppingListsEndpoint(
        ILogger<GetAllShoppingListsEndpoint> logger,
        IGetAllShoppingListsQuery getAllShoppingListsQuery)
    {
        _logger = logger;
        _getAllShoppingListsQuery = getAllShoppingListsQuery;
    }

    [HttpGet("api/v1/lists")]
    public async Task<ActionResult<Response<ShoppingListDto[]>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
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