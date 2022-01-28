using Shopperior.Application.ListItems.Models;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ListItems.Resolvers;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using Shopperior.Domain.Enumerations;

namespace Shopperior.Application.ListItems.Resolvers;

public class ListItemModelResolver : IListItemModelResolver
{
    private readonly IListItemRepository _listItemRepository;
    private readonly IShoppingListRepository _shoppingListRepository;

    public ListItemModelResolver(
        IListItemRepository listItemRepository,
        IShoppingListRepository shoppingListRepository)
    {
        _listItemRepository = listItemRepository;
        _shoppingListRepository = shoppingListRepository;
    }

    public async Task<IListItemModel> ResolveAsync(ListItem entity)
    {
        if (entity == null) return null;

        var shoppingList = await _shoppingListRepository.GetOneAsync(entity.ShoppingListId);
        var shoppingListModel = (shoppingList != null)
            ? new ShoppingListModel
            {
                Guid = shoppingList.Guid,
                Name = shoppingList.Name
            }
            : null;
        var model = new ListItemModel
        {
            Guid = entity.Guid,
            ShoppingList = shoppingListModel,
            Quantity = entity.Quantity,
            Measurement = Measurement.FromName(entity.Measurement),
            UnitPrice = entity.UnitPrice,
            TotalPrice = entity.TotalPrice,
            IsInCart = entity.IsInCart,
            EnteredCartTime = entity.EnteredCartTime,
            HasPurchased = entity.HasPurchased,
            PurchasedTime = entity.PurchasedTime
        };

        return model;
    }

    public async Task<ListItem> ResolveAsync(IListItemModel model)
    {
        if (model == null) return null;

        var existing = await _listItemRepository.GetOne(model.Guid);
        var shoppingList = await _shoppingListRepository.GetOneAsync(model.ShoppingList.Guid);
        var entity = new ListItem
        {
            Id = existing?.Id ?? 0,
            Guid = model.Guid,
            ShoppingListId = shoppingList?.Id ?? 0,
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
            EnteredCartTime = model.EnteredCartTime,
            HasPurchased = model.HasPurchased,
            PurchasedTime = model.PurchasedTime,
            CreatedTime = existing?.CreatedTime ?? default,
            LastModifiedTime = existing?.LastModifiedTime ?? default,
            TrashedTime = existing?.TrashedTime ?? default
        };

        return entity;
    }
}