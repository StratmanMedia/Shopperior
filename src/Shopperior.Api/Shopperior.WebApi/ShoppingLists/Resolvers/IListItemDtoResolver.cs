using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Resolvers;

public interface IListItemDtoResolver
{
    Task<ListItemDto> ResolveAsync(IListItemModel model);
    Task<IListItemModel> ResolveAsync(ListItemDto dto);
}