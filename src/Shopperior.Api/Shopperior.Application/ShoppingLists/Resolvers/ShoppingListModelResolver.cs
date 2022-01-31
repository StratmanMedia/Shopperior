using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Resolvers;

public class ShoppingListModelResolver : IShoppingListModelResolver
{
    private readonly IUserListPermissionRepository _listPermissionRepository;
    private readonly IListItemRepository _listItemRepository;
    private readonly IListPermissionModelResolver _listPermissionModelResolver;
    private readonly IListItemModelResolver _listItemModelResolver;

    public ShoppingListModelResolver(
        IUserListPermissionRepository listPermissionRepository,
        IListItemRepository listItemRepository,
        IListPermissionModelResolver listPermissionModelResolver,
        IListItemModelResolver listItemModelResolver)
    {
        _listPermissionRepository = listPermissionRepository;
        _listItemRepository = listItemRepository;
        _listPermissionModelResolver = listPermissionModelResolver;
        _listItemModelResolver = listItemModelResolver;
    }

    public async Task<IShoppingListModel> ResolveAsync(ShoppingList entity)
    {
        var permissions = await GetListPermissionsAsync(entity.Id);
        var items = await GetListItemsAsync(entity.Id);

        var model = new ShoppingListModel
        {
            Guid = entity.Guid,
            Name = entity.Name,
            Permissions = permissions,
            Items = items
        };

        return model;
    }

    public Task<ShoppingList> ResolveAsync(IShoppingListModel model)
    {
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<IUserListPermissionModel>> GetListPermissionsAsync(long listId)
    {
        var permissions = await _listPermissionRepository.GetManyByListAsync(listId);
        var permissionModels = new List<IUserListPermissionModel>();
        foreach (var p in permissions.ToArray())
        {
            var permissionModel = await _listPermissionModelResolver.ResolveAsync(p);
            permissionModels.Add(permissionModel);
        }

        return permissionModels;
    }

    private async Task<IEnumerable<IListItemModel>> GetListItemsAsync(long listId)
    {
        var items = await _listItemRepository.GetManyByListAsync(listId);
        var itemModels = new List<IListItemModel>();
        foreach (var i in items.ToArray())
        {
            var itemModel = await _listItemModelResolver.ResolveAsync(i);
            itemModels.Add(itemModel);
        }

        return itemModels;
    }
}