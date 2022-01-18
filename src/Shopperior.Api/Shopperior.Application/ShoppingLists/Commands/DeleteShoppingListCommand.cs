using Microsoft.Extensions.Logging;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Enumerations;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Application.ShoppingLists.Commands;

public class DeleteShoppingListCommand : IDeleteShoppingListCommand
{
    private readonly ILogger<DeleteShoppingListCommand> _logger;
    private readonly IValidateShoppingListPermissionQuery _validateShoppingListPermissionQuery;
    private readonly IUserListPermissionRepository _userListPermissionRepository;
    private readonly IShoppingListRepository _shoppingListRepository;

    public DeleteShoppingListCommand(
        ILogger<DeleteShoppingListCommand> logger,
        IValidateShoppingListPermissionQuery validateShoppingListPermissionQuery,
        IUserListPermissionRepository userListPermissionRepository,
        IShoppingListRepository shoppingListRepository)
    {
        _logger = logger;
        _validateShoppingListPermissionQuery = validateShoppingListPermissionQuery;
        _userListPermissionRepository = userListPermissionRepository;
        _shoppingListRepository = shoppingListRepository;
    }
    public async Task<Response> ExecuteAsync(IDeleteShoppingListRequest request, CancellationToken ct = new())
    {
        try
        {
            var shoppingList = await _shoppingListRepository.GetOneAsync(request.ShoppingListGuid, ct);
            var permission = await _validateShoppingListPermissionQuery.ExecuteAsync(
                request.UserGuid,
                shoppingList,
                ShoppingListPermission.Administrator.Name,
                ct);
            var hasPermission = permission.IsSuccess;

            if (!hasPermission)
                return new Response($"User ({request.UserGuid}) does not have permission to delete shopping list ({request.ShoppingListGuid})");

            var permissions = await _userListPermissionRepository.GetManyByListAsync(shoppingList.Id, ct);
            await _userListPermissionRepository.DeleteRangeAsync(permissions, ct);

            await _shoppingListRepository.DeleteAsync(shoppingList, ct);

            return new Response();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return new Response("A problem occurred while trying to create the shopping list.");
        }
    }
}