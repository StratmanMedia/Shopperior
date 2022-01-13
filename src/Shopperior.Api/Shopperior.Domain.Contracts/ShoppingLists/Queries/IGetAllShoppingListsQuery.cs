using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Queries;

public interface IGetAllShoppingListsQuery : 
    IBaseQuery<string, IEnumerable<ShoppingList>>, 
    IBaseQuery<long, IEnumerable<ShoppingList>>,
    IBaseQuery<Guid, IEnumerable<ShoppingList>>
{
}