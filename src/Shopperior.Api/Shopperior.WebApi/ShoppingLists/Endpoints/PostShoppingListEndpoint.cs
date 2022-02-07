using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Entities;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.ShoppingLists.Endpoints;

public class PostShoppingListEndpoint : BaseEndpoint
{
    private readonly ICurrentUserProvider _currentUserService;
    private readonly ILogger<PostShoppingListEndpoint> _logger;
    private readonly ICreateShoppingListCommand _createShoppingListCommand;

    public PostShoppingListEndpoint(
        ILogger<PostShoppingListEndpoint> logger,
        ICurrentUserProvider currentUserService,
        ICreateShoppingListCommand createShoppingListCommand)
    {
        _currentUserService = currentUserService;
        _logger = Guard.Against.Null(logger, nameof(logger));
        _createShoppingListCommand = Guard.Against.Null(createShoppingListCommand, nameof(createShoppingListCommand));
    }

    [Authorize("shop.api.access")]
    [HttpPost("api/v1/lists")]
    public async Task<ActionResult<Response>> HandleAsync([FromBody] ShoppingListDto dto, CancellationToken cancellationToken = new())
    {
        try
        {
            var validation = await ValidateRequest(dto);
            if (!validation.IsSuccess) return BadRequest(validation.Messages);

            var currentUser = _currentUserService.CurrentUser;
            if (currentUser == null) return Unauthorized();

            var shoppingList = new ShoppingList
            {
                Name = dto.Name
            };
            var request = new CreateShoppingListRequest
            {
                UserGuid = currentUser.Guid,
                ShoppingList = shoppingList
            };
            var result = await _createShoppingListCommand.ExecuteAsync(request, cancellationToken);
            if (!result.IsSuccess) return BadRequest(result.Messages);

            return Created($"/api/v1/{result.Data.Guid}", new Response());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return StatusCode(500);
        }
    }

    private async Task<Response> ValidateRequest(ShoppingListDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new Response($"Shopping List {nameof(request.Name)} is missing.");

        return new Response();
    }
}