using Shopperior.Application.ListItems.Models;
using Shopperior.Domain.Contracts.Categories.Queries;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Resolvers;

public class ListItemDtoResolver : IListItemDtoResolver
{
    private readonly IGetOneShoppingListQuery _getOneShoppingListQuery;
    private readonly IGetOneCategoryQuery _getOneCategoryQuery;

    public ListItemDtoResolver(
        IGetOneShoppingListQuery getOneShoppingListQuery,
        IGetOneCategoryQuery getOneCategoryQuery)
    {
        _getOneShoppingListQuery = getOneShoppingListQuery;
        _getOneCategoryQuery = getOneCategoryQuery;
    }
    public async Task<ListItemDto> ResolveAsync(IListItemModel model)
    {
        if (model == null) return null;

        var dto = new ListItemDto
        {
            Guid = model.Guid,
            ShoppingListGuid = model.ShoppingListGuid,
            CategoryGuid = model.Category?.Guid ?? Guid.Empty,
            Name = model.Name,
            Brand = model.Brand,
            Comment = model.Comment,
            Quantity = model.Quantity,
            Measurement = model.Measurement.ToString(),
            UnitPrice = model.UnitPrice,
            TotalPrice = model.TotalPrice,
            IsInCart = model.IsInCart,
            HasPurchased = model.HasPurchased
        };

        return await Task.FromResult(dto);
    }

    public async Task<IListItemModel> ResolveAsync(ListItemDto dto)
    {
        if (dto == null) return null;

        var measurement = Measurement.FromName(dto.Measurement);
        var model = new ListItemModel
        {
            Guid = dto.Guid,
            ShoppingListGuid = dto.ShoppingListGuid,
            Category = await _getOneCategoryQuery.ExecuteAsync(dto.CategoryGuid),
            Name = dto.Name,
            Brand = dto.Brand,
            Comment = dto.Comment,
            Quantity = dto.Quantity,
            Measurement = measurement,
            UnitPrice = dto.UnitPrice,
            TotalPrice = dto.TotalPrice,
            IsInCart = dto.IsInCart,
            HasPurchased = dto.HasPurchased
        };

        return await Task.FromResult((IListItemModel)model);
    }
}