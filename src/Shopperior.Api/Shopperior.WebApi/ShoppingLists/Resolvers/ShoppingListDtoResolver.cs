using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Resolvers;

public class ShoppingListDtoResolver : IShoppingListDtoResolver
{
    private readonly IListPermissionDtoResolver _listPermissionDtoResolver;
    private readonly IListItemDtoResolver _listItemDtoResolver;

    public ShoppingListDtoResolver(
        IListPermissionDtoResolver listPermissionDtoResolver,
        IListItemDtoResolver listItemDtoResolver)
    {
        _listPermissionDtoResolver = listPermissionDtoResolver;
        _listItemDtoResolver = listItemDtoResolver;
    }

    public async Task<ShoppingListDto> ResolveAsync(IShoppingListModel model, CancellationToken ct = default)
    {
        var permissions = new List<UserListPermissionDto>();
        foreach (var p in model.Permissions)
        {
            permissions.Add(await _listPermissionDtoResolver.ResolveAsync(p, ct));
        }
        var items = new List<ListItemDto>();
        foreach (var i in model.Items)
        {
            items.Add(await _listItemDtoResolver.ResolveAsync(i));
        }
        var dto = new ShoppingListDto
        {
            Guid = model.Guid,
            Name = model.Name,
            Permissions = permissions,
            Items = items
        };

        return dto;
    }
}