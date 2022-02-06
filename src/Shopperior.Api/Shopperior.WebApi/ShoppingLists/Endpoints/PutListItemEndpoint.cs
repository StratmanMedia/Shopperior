using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class PutListItemEndpoint : BaseEndpoint
{
    private readonly ILogger<PutListItemEndpoint> _logger;
    private readonly IListItemDtoResolver _listItemDtoResolver;
    private readonly IUpdateListItemCommand _updateListItemCommand;

    public PutListItemEndpoint(
        ILogger<PutListItemEndpoint> logger,
        IListItemDtoResolver listItemDtoResolver,
        IUpdateListItemCommand updateListItemCommand)
    : base(logger)
    {
        _logger = logger;
        _listItemDtoResolver = listItemDtoResolver;
        _updateListItemCommand = updateListItemCommand;
    }

    [Authorize]
    [HttpPut("api/v1/lists/{listGuid}/items/{itemGuid}")]
    public async Task<ActionResult<Response>> HandleAsync(Guid listGuid, Guid itemGuid, CancellationToken ct = new CancellationToken())
    {
        return await TryActionAsync(() => EndpointAction(listGuid, itemGuid, ct));
    }

    private async Task EndpointAction(Guid listGuid, Guid itemGuid, CancellationToken ct = new CancellationToken())
    {
        var dto = await GetRequestBody<ListItemDto>();
        await ValidateRequest(listGuid, itemGuid, dto, ct);

        var model = await _listItemDtoResolver.ResolveAsync(dto);
        await _updateListItemCommand.ExecuteAsync(model, ct);
    }

    private Task ValidateRequest(Guid listGuid, Guid itemGuid, ListItemDto dto, CancellationToken ct = new CancellationToken())
    {
        if (dto.Guid != itemGuid)
            throw new BadHttpRequestException("Item GUID in URI did not match the request body.");

        if (dto.ShoppingListGuid != listGuid)
            throw new BadHttpRequestException("List GUID in URI did not match the request body.");

        if (dto.Guid == Guid.Empty)
            throw new BadHttpRequestException("Item GUID was empty.");

        if (dto.ShoppingListGuid == Guid.Empty)
            throw new BadHttpRequestException("List GUID was empty.");

        return Task.CompletedTask;
    }
}