using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;
using Shopperior.Domain.Enumerations;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Application.ShoppingLists.Commands;

public class UpdateShoppingListCommand : IUpdateShoppingListCommand
{
    private readonly ILogger<UpdateShoppingListCommand> _logger;
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IUserListPermissionRepository _userListPermissionRepository;
    private readonly IGetOneUserQuery _getOneUserQuery;
    private readonly IGetOneShoppingListQuery _getOneShoppingListQuery;

    public UpdateShoppingListCommand(
        ILogger<UpdateShoppingListCommand> logger,
        IShoppingListRepository shoppingListRepository,
        IUserListPermissionRepository userListPermissionRepository,
        IGetOneUserQuery getOneUserQuery,
        IGetOneShoppingListQuery getOneShoppingListQuery)
    {
        _logger = logger;
        _shoppingListRepository = shoppingListRepository;
        _userListPermissionRepository = userListPermissionRepository;
        _getOneUserQuery = getOneUserQuery;
        _getOneShoppingListQuery = getOneShoppingListQuery;
    }

    public async Task<Response> ExecuteAsync(IShoppingListModel request, CancellationToken ct = new())
    {
        try
        {
            var validation = await ValidateRequest(request, ct);
            if (!validation.IsSuccess) return new Response(validation.Messages);

            var shoppingList = await _getOneShoppingListQuery.ExecuteAsync(request.Guid, ct);
            if (shoppingList == null)
                return new Response($"Shopping List with {nameof(request.Guid)} of {request.Guid} was not found.");

            shoppingList.Name = request.Name;
            shoppingList.LastModifiedTime = DateTime.UtcNow;
            await _shoppingListRepository.UpdateAsync(shoppingList, ct);

            var newPermissions = new List<UserListPermission>();
            foreach (var permission in request.Permissions)
            {
                var user = await _getOneUserQuery.ExecuteAsync(permission.UserGuid, ct);
                if (user == null)
                    return new Response($"User with {nameof(permission.UserGuid)} of {permission.UserGuid} was not found.");

                var shoppingListPermission = ShoppingListPermission.FromName(permission.Permission, true).ToString();
                newPermissions.Add(new UserListPermission
                {
                    Id = 0,
                    UserId = user.Id,
                    ShoppingListId = shoppingList.Id,
                    Permission = shoppingListPermission,
                    CreatedTime = DateTime.UtcNow
                });
            }

            var currentPermissions = (await _userListPermissionRepository.GetManyByListAsync(shoppingList.Id, ct)).ToArray();

            foreach (var newPermission in newPermissions)
            {
                var existing = currentPermissions.FirstOrDefault(p => p.UserId == newPermission.UserId && p.ShoppingListId == newPermission.ShoppingListId);
                if (existing != null)
                {
                    existing.Permission = newPermission.Permission;
                    await _userListPermissionRepository.UpdateAsync(existing, ct);
                    continue;
                }

                await _userListPermissionRepository.CreateAsync(newPermission, ct);
            }

            foreach (var currentPermission in currentPermissions)
            {
                var removed = !newPermissions.Any(p => p.UserId == currentPermission.UserId && p.ShoppingListId == currentPermission.ShoppingListId);
                if (removed)
                {
                    await _userListPermissionRepository.DeleteAsync(currentPermission, ct);
                }
            }




            return new Response();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return new Response("A problem occurred while trying to create the shopping list.");
        }
    }

    private async Task<Response> ValidateRequest(IShoppingListModel request, CancellationToken ct = new())
    {
        return await Task.Run(() =>
        {
            if (request.Guid == Guid.Empty)
                return new Response("Invalid Shopping List GUID was provided.");

            if (string.IsNullOrWhiteSpace(request.Name))
                return new Response("Shopping List name must be provided.");

            if (request.Permissions.Count(p => p.Permission.ToUpper() == ShoppingListPermission.Owner.ToString()) != 1)
                return new Response("Permissions must include one Owner.");

            return new Response();
        }, ct);
    }
}