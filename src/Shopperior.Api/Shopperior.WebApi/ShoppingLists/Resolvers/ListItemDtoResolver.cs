﻿using Shopperior.Application.ListItems.Models;
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
        if (model == null) return null;

        var dto = new ListItemDto
        {
            Guid = model.Guid,
            ShoppingListGuid = model.ShoppingListGuid,
            Name = model.Name,
            Brand = model.Brand,
            Comment = model.Comment,
            Quantity = model.Quantity,
            Measurement = model.Measurement.ToString(),
            UnitPrice = model.UnitPrice,
            TotalPrice = model.TotalPrice,
            IsInCart = model.IsInCart,
            EnteredCartTime = model.EnteredCartTime,
            HasPurchased = model.HasPurchased,
            PurchasedTime = model.PurchasedTime
        };

        return Task.FromResult(dto);
    }

    public Task<IListItemModel> ResolveAsync(ListItemDto dto)
    {
        if (dto == null) return null;

        var measurement = Measurement.FromName(dto.Measurement);
        var model = new ListItemModel
        {
            Guid = dto.Guid,
            ShoppingListGuid = dto.ShoppingListGuid,
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

        return Task.FromResult((IListItemModel)model);
    }
}