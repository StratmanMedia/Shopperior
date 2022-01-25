using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Queries;

public interface IGetAllShoppingListsQuery : 
    IBaseQuery<string, IEnumerable<IShoppingListModel>>, 
    IBaseQuery<long, IEnumerable<IShoppingListModel>>,
    IBaseQuery<Guid, IEnumerable<IShoppingListModel>>
{
}