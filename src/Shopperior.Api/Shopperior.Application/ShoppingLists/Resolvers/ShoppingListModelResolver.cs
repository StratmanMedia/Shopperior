using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;
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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IListItemRepository _listItemRepository;
    private readonly IListPermissionModelResolver _listPermissionModelResolver;
    private readonly ICategoryModelResolver _categoryModelResolver;
    private readonly IListItemModelResolver _listItemModelResolver;

    public ShoppingListModelResolver(
        IUserListPermissionRepository listPermissionRepository,
        ICategoryRepository categoryRepository,
        IListItemRepository listItemRepository,
        IListPermissionModelResolver listPermissionModelResolver,
        ICategoryModelResolver categoryModelResolver,
        IListItemModelResolver listItemModelResolver)
    {
        _listPermissionRepository = listPermissionRepository;
        _categoryRepository = categoryRepository;
        _listItemRepository = listItemRepository;
        _listPermissionModelResolver = listPermissionModelResolver;
        _categoryModelResolver = categoryModelResolver;
        _listItemModelResolver = listItemModelResolver;
    }

    public async Task<IShoppingListModel> ResolveAsync(ShoppingList entity)
    {
        if (entity == null) return null;

        var permissions = await GetListPermissionsAsync(entity);
        var categories = await GetCategoriesAsync(entity);
        var items = await GetListItemsAsync(entity);

        var model = new ShoppingListModel
        {
            Guid = entity.Guid,
            Name = entity.Name,
            Permissions = permissions,
            Categories = categories,
            Items = items
        };

        return model;
    }

    public Task<ShoppingList> ResolveAsync(IShoppingListModel model)
    {
        throw new NotImplementedException();
    }

    private async Task<IEnumerable<IUserListPermissionModel>> GetListPermissionsAsync(ShoppingList entity)
    {
        var permissions = (entity.Permissions.Any())
            ? entity.Permissions.ToArray()
            : await _listPermissionRepository.GetManyByListAsync(entity.Id);
        var permissionModels = new List<IUserListPermissionModel>();
        foreach (var p in permissions)
        {
            var permissionModel = await _listPermissionModelResolver.ResolveAsync(p);
            permissionModels.Add(permissionModel);
        }

        return permissionModels;
    }

    private async Task<IEnumerable<ICategoryModel>> GetCategoriesAsync(ShoppingList entity)
    {
        var categories = (entity.Categories.Any())
            ? entity.Categories.ToArray()
            : await _categoryRepository.GetManyByShoppingList(entity.Guid);
        var categoryModels = new List<ICategoryModel>();
        foreach (var c in categories)
        {
            var categoryModel = await _categoryModelResolver.ResolveAsync(c);
            categoryModels.Add(categoryModel);
        }

        return categoryModels;
    }

    private async Task<IEnumerable<IListItemModel>> GetListItemsAsync(ShoppingList entity)
    {
        var items = (entity.Items.Any())
            ? entity.Items.ToArray()
            : await _listItemRepository.GetManyByListAsync(entity.Id);
        var itemModels = new List<IListItemModel>();
        foreach (var i in items)
        {
            var itemModel = await _listItemModelResolver.ResolveAsync(i);
            itemModels.Add(itemModel);
        }

        return itemModels;
    }
}