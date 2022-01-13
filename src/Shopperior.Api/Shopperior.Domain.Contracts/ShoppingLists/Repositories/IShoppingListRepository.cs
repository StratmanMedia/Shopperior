using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IShoppingListRepository : IRepository<ShoppingList>
{
    Task<IEnumerable<ShoppingList>> GetManyByUserAsync(string username, CancellationToken cancellationToken = new());
}