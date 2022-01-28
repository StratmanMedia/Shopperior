using Shopperior.Application.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Resolvers;

public class ListItemDtoResolver : IListItemDtoResolver
{
    private readonly IGetOneShoppingListQuery _getOneShoppingListQuery;

    public ListItemDtoResolver(
        IGetOneShoppingListQuery getOneShoppingListQuery)
    {
        _getOneShoppingListQuery = getOneShoppingListQuery;
    }
    public Task<ListItemDto> ResolveAsync(IListItemModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<IListItemModel> ResolveAsync(ListItemDto dto)
    {
        if (dto == null) return null;

        var shoppingList = await _getOneShoppingListQuery.ExecuteAsync(dto.ShoppingListGuid);
        var measurement = Measurement.FromName(dto.Measurement);
        var model = new ListItemModel
        {
            Guid = dto.Guid,
            ShoppingList = shoppingList,
            Name = dto.Name,
            Brand = dto.Brand,
            Comment = dto.Comment,
            Quantity = dto.Quantity,
            Measurement = measurement,
            UnitPrice = dto.UnitPrice,
            TotalPrice = dto.TotalPrice,
            IsInCart = dto.IsInCart,
            EnteredCartTime = dto.EnteredCartTime,
            HasPurchased = dto.HasPurchased,
            PurchasedTime = dto.PurchasedTime
        };

        return model;
    }
}