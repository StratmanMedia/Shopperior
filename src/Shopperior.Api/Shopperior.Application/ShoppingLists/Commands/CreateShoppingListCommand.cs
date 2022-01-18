using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;
using Shopperior.Domain.Enumerations;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Application.ShoppingLists.Commands;

public class CreateShoppingListCommand : ICreateShoppingListCommand
{
    private readonly IUserListPermissionRepository _userListRepository;
    private readonly ILogger<CreateShoppingListCommand> _logger;
    private readonly IGetOneUserQuery _getOneUserQuery;
    private readonly IShoppingListRepository _shoppingListRepository;

    public CreateShoppingListCommand(
        ILogger<CreateShoppingListCommand> logger,
        IGetOneUserQuery getOneUserQuery,
        IShoppingListRepository shoppingListRepository,
        IUserListPermissionRepository userListRepository)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _getOneUserQuery = Guard.Against.Null(getOneUserQuery, nameof(getOneUserQuery));
        _shoppingListRepository = Guard.Against.Null(shoppingListRepository, nameof(shoppingListRepository));
        _userListRepository = Guard.Against.Null(userListRepository, nameof(userListRepository));
    }

    public async Task<Response<ShoppingList>> ExecuteAsync(ICreateShoppingListRequest request, CancellationToken ct)
    {
        try
        {
            var validation = await ValidateRequest(request);
            if (!validation.IsSuccess) return new Response<ShoppingList>(validation.Messages);

            var user = await _getOneUserQuery.ExecuteAsync(request.UserGuid, ct);
            if (user == null) return new Response<ShoppingList>("User was not found.");

            var entity = new ShoppingList
            {
                Guid = Guid.NewGuid(),
                Name = request.ShoppingList.Name,
                CreatedTime = DateTime.UtcNow
            };
            await _shoppingListRepository.CreateAsync(entity, ct);

            var ownerPermission = new UserListPermission
            {
                Guid = Guid.NewGuid(),
                UserId = user.Id,
                ShoppingListId = entity.Id,
                Permission = ShoppingListPermission.Owner.ToString(),
                CreatedTime = DateTime.UtcNow
            };
            await _userListRepository.CreateAsync(ownerPermission, ct);

            return new Response<ShoppingList>(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return new Response<ShoppingList>("A problem occurred while trying to create the shopping list.");
        }
    }

    private Task<Response> ValidateRequest(ICreateShoppingListRequest request)
    {
        if (request.UserGuid == Guid.Empty)
            return Task.FromResult(new Response("Invalid User GUID was provided."));

        if (request.ShoppingList == null)
            return Task.FromResult(new Response("Shopping List data was not provided."));

        return Task.FromResult(new Response());
    }
}