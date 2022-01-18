using Microsoft.Extensions.Logging;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;
using Shopperior.Domain.Enumerations;
using Shopperior.Domain.Exceptions;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Application.ShoppingLists.Queries;

public class ValidateShoppingListPermissionQuery : IValidateShoppingListPermissionQuery
{
    private readonly ILogger<ValidateShoppingListPermissionQuery> _logger;
    private readonly IUserListPermissionRepository _userListPermissionRepository;
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IUserRepository _userRepository;

    public ValidateShoppingListPermissionQuery(
        ILogger<ValidateShoppingListPermissionQuery> logger,
        IUserListPermissionRepository userListPermissionRepository,
        IShoppingListRepository shoppingListRepository,
        IUserRepository userRepository)
    {
        _logger = logger;
        _userListPermissionRepository = userListPermissionRepository;
        _shoppingListRepository = shoppingListRepository;
        _userRepository = userRepository;
    }

    public async Task<Response> ExecuteAsync(IValidateShoppingListPermissionRequest request, CancellationToken ct = new())
    {
        try
        {
            var listPermissions = await _userListPermissionRepository.GetManyByListAsync(request.ShoppingList.Id, ct);
            var userListPermissions = listPermissions.Where(p => p.UserId == request.User.Id).ToArray();
            if (userListPermissions.Count() > 1)
                throw new ReferentialIntegrityException($"Multiple permissions found for user ({request.User.Guid}) and list {request.ShoppingList.Guid}).");

            var userListPermission = userListPermissions.FirstOrDefault();
            if (userListPermission == null)
                return new Response("User does not have permission for this list.");

            var userPermission = ShoppingListPermission.FromName(userListPermission.Permission);
            var requestedPermission = ShoppingListPermission.FromName(request.Permission);

            if (userPermission.Value < requestedPermission.Value)
                return new Response("User does not have the required permission for this list.");

            return new Response();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return new Response("A problem occurred while trying to validate the user's shopping list permissions.");
        }
    }

    public async Task<Response> ExecuteAsync(User user, Guid shoppingListGuid, string permission, CancellationToken ct = new())
    {
        var shoppingList = await _shoppingListRepository.GetOneAsync(shoppingListGuid, ct);
        if (shoppingList == null) return new Response("No shopping list matches the provided GUID.");

        return await ExecuteAsync(new ValidateShoppingListPermissionRequest
        {
            User = user,
            ShoppingList = shoppingList,
            Permission = permission
        }, ct);
    }

    public async Task<Response> ExecuteAsync(Guid userGuid, ShoppingList shoppingList, string permission, CancellationToken ct = new())
    {
        var user = await _userRepository.GetAsync(userGuid);
        if (user == null) return new Response("No user matches the provided GUID.");

        return await ExecuteAsync(new ValidateShoppingListPermissionRequest
        {
            User = user,
            ShoppingList = shoppingList,
            Permission = permission
        }, ct);
    }
}