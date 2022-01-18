using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Users.Resolvers;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class DeleteShoppingListEndpoint : BaseEndpoint
{
    private readonly ILogger<DeleteShoppingListEndpoint> _logger;
    private readonly ICurrentUserResolver _currentUserResolver;
    private readonly IDeleteShoppingListCommand _deleteShoppingListCommand;

    public DeleteShoppingListEndpoint(
        ILogger<DeleteShoppingListEndpoint> logger,
        ICurrentUserResolver currentUserResolver,
        IDeleteShoppingListCommand deleteShoppingListCommand)
    {
        _logger = logger;
        _currentUserResolver = currentUserResolver;
        _deleteShoppingListCommand = deleteShoppingListCommand;
    }

    [Authorize]
    [HttpDelete("/api/v1/lists/{guid}")]
    public async Task<ActionResult<Response>> DeleteShoppingList(Guid guid, CancellationToken ct = new())
    {
        try
        {
            var validation = await ValidateRequest(guid);
            if (!validation.IsSuccess) return BadRequest(validation.Messages);

            var currentUser = await _currentUserResolver.Resolve(User, GetAuthorizationHeaderValue(), ct);
            if (currentUser == null) return Unauthorized();

            var result = await _deleteShoppingListCommand.ExecuteAsync(new DeleteShoppingListRequest
            {
                UserGuid = currentUser.Guid,
                ShoppingListGuid = guid
            }, ct);
            if (!result.IsSuccess) throw new InvalidOperationException(string.Join('|', result.Messages));

            return Ok(new Response());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return StatusCode(500);
        }
    }

    private async Task<Response> ValidateRequest(Guid request)
    {
        if (request == Guid.Empty)
            return new Response("Invalid list GUID was provided.");

        return new Response();
    }
}