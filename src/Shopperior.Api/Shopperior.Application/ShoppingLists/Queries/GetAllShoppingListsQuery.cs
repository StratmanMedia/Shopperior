using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Queries;

public class GetAllShoppingListsQuery : IGetAllShoppingListsQuery
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IUserListPermissionRepository _userListPermissionRepository;
    private readonly IGetOneUserQuery _getOneUserQuery;

    public GetAllShoppingListsQuery(
        IShoppingListRepository shoppingListRepository,
        IUserListPermissionRepository userListPermissionRepository,
        IGetOneUserQuery getOneUserQuery)
    {
        _shoppingListRepository = shoppingListRepository;
        _userListPermissionRepository = userListPermissionRepository;
        _getOneUserQuery = getOneUserQuery;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(string username, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(username, ct);
        var lists = await GetLists(user, ct);

        return lists;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(long userId, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(userId, ct);
        var lists = await GetLists(user, ct);

        return lists;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(Guid userGuid, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(userGuid, ct);
        var lists = await GetLists(user, ct);

        return lists;
    }

    private async Task<IEnumerable<IShoppingListModel>> GetLists(User user, CancellationToken ct = new()) {
        var entities = await _shoppingListRepository.GetManyByUserAsync(user.Id, ct);
        var lists = new List<IShoppingListModel>();
        foreach (var entity in entities.ToArray())
        {
            lists.Add(await BuildModel(entity, ct));
        }

        return lists;
    } 

    private async Task<ShoppingListModel> BuildModel(ShoppingList list, CancellationToken ct = new())
    {
        var permissions = await _userListPermissionRepository.GetManyByListAsync(list.Id, ct);
        var permissionModels = new List<IUserListPermissionModel>();
        foreach (var p in permissions.ToArray())
        {
            var permissionModel = await BuildModel(p, list, ct);
            permissionModels.Add(permissionModel);
        }
        var model = new ShoppingListModel
        {
            Guid = list.Guid,
            Name = list.Name,
            Permissions = permissionModels
        };

        return model;
    }

    private async Task<UserListPermissionModel> BuildModel(UserListPermission permission, ShoppingList list, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(permission.UserId, ct);
        var listModel = new ShoppingListModel
        {
            Guid = list.Guid,
            Name = list.Name
        };
        var model = new UserListPermissionModel
        {
            User = user,
            ShoppingList = listModel,
            Permission = permission.Permission
        };

        return model;
    }
}