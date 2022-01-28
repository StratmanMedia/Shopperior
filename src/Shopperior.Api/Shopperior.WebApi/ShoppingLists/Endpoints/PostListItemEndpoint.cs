using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopperior.Domain.Contracts.ListItems.Commands;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class PostListItemEndpoint : BaseEndpoint
{
    private readonly ILogger<PostListItemEndpoint> _logger;
    private readonly IListItemDtoResolver _listItemDtoResolver;
    private readonly ICreateListItemCommand _createListItemCommand;

    public PostListItemEndpoint(
        ILogger<PostListItemEndpoint> logger,
        IListItemDtoResolver listItemDtoResolver,
        ICreateListItemCommand createListItemCommand)
    {
        _logger = logger;
        _listItemDtoResolver = listItemDtoResolver;
        _createListItemCommand = createListItemCommand;
    }

    [Authorize]
    [HttpPost("api/v1/lists/{listGuid}/items")]
    public async Task<ActionResult<Response>> HandleAsync(Guid listGuid, CancellationToken ct = new CancellationToken())
    {
        try
        {
            var dto = await GetRequestBody<ListItemDto>();
            var validation = await ValidateRequest(listGuid, dto);
            if (!validation.IsSuccess)
                return BadRequest(new Response(validation.Messages));

            var model = await _listItemDtoResolver.ResolveAsync(dto);
            await _createListItemCommand.ExecuteAsync(model, ct);

            //TODO: Get list item and add guid to createdAt uri
            return Created($"/api/v1/lists/{listGuid}", new Response());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return InternalServerError();
        }
    }

    private async Task<Response> ValidateRequest(Guid listGuid, ListItemDto dto)
    {
        if (listGuid == Guid.Empty)
            return new Response("The Shopping List GUID provided in the URI is invalid.");

        if (dto == null)
            return new Response("The request body was malformed.");

        if (listGuid != dto.ShoppingListGuid)
            return new Response("The GUID in the URI and ShoppingListGuid in the request body do not match.");

        if (dto.Quantity <= 0)
            return new Response("The request body did not contain Quantity.");

        if (string.IsNullOrWhiteSpace(dto.Measurement))
            return new Response("The request body did not contain Measurement.");

        return new Response();
    }
}