using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Queries;

public interface IGetOneShoppingListQuery : IBaseQuery<long, ShoppingList>, IBaseQuery<Guid, ShoppingList>
{
    
}