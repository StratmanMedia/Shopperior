using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ListItems.Resolvers;

public interface IListItemModelResolver
{
    Task<IListItemModel> ResolveAsync(ListItem entity);
    Task<ListItem> ResolveAsync(IListItemModel model);
}