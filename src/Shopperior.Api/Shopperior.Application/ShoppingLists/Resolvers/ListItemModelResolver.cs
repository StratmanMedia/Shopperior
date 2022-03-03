using Shopperior.Application.ListItems.Models;
using Shopperior.Domain.Contracts.Categories.Services;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;
using Shopperior.Domain.Entities;
using Shopperior.Domain.Enumerations;

namespace Shopperior.Application.ShoppingLists.Resolvers;

public class ListItemModelResolver : IListItemModelResolver
{
    private readonly IListItemRepository _listItemRepository;
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly ICategoryModelResolver _categoryModelResolver;

    public ListItemModelResolver(
        IListItemRepository listItemRepository,
        IShoppingListRepository shoppingListRepository,
        ICategoryModelResolver categoryModelResolver)
    {
        _listItemRepository = listItemRepository;
        _shoppingListRepository = shoppingListRepository;
        _categoryModelResolver = categoryModelResolver;
    }

    public async Task<IListItemModel> ResolveAsync(ListItem entity)
    {
        if (entity == null) return null;

        var shoppingList = entity.ShoppingList ?? await _shoppingListRepository.GetOneAsync(entity.ShoppingListId);
        var model = new ListItemModel
        {
            Guid = entity.Guid,
            ShoppingListGuid = shoppingList.Guid,
            Category = await _categoryModelResolver.ResolveAsync(entity.Category),
            Name = entity.Name,
            Brand = entity.Brand,
            Comment = entity.Comment,
            Quantity = entity.Quantity,
            Measurement = Measurement.FromName(entity.Measurement),
            UnitPrice = entity.UnitPrice,
            TotalPrice = entity.TotalPrice,
            IsInCart = entity.IsInCart,
            HasPurchased = entity.HasPurchased,
            PurchasedTime = entity.PurchasedTime
        };

        return model;
    }

    public async Task<ListItem> ResolveAsync(IListItemModel model)
    {
        if (model == null) return null;

        var existing = await _listItemRepository.GetOne(model.Guid);
        if (existing == null)
        {
            var shoppingList = await _shoppingListRepository.GetOneAsync(model.ShoppingListGuid);
            var entity = new ListItem
            {
                Id = default,
                Guid = model.Guid,
                ShoppingListId = shoppingList?.Id ?? 0,
                ShoppingList = shoppingList,
                Category = await _categoryModelResolver.ResolveAsync(model.Category),
                ItemId = 0,
                Name = model.Name,
                Brand = model.Brand,
                Comment = model.Comment,
                StoreId = 0,
                CategoryId = 0,
                Quantity = model.Quantity,
                Measurement = model.Measurement.ToString(),
                UnitPrice = model.UnitPrice,
                TotalPrice = model.TotalPrice,
                IsInCart = model.IsInCart,
                HasPurchased = model.HasPurchased,
                PurchasedTime = model.PurchasedTime
            };

            return entity;
        }

        existing.Name = model.Name;
        existing.Brand = model.Brand;
        existing.Comment = model.Comment;
        existing.Quantity = model.Quantity;
        existing.Measurement = model.Measurement.ToString();
        existing.UnitPrice = model.UnitPrice;
        existing.TotalPrice = model.TotalPrice;
        existing.IsInCart = model.IsInCart;
        existing.HasPurchased = model.HasPurchased;

        return existing;
    }
}