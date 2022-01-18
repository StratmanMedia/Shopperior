using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IShoppingListRepository : IRepository<ShoppingList>
{
    Task<ShoppingList> GetOneAsync(Guid guid, CancellationToken ct = new());
    Task<IEnumerable<ShoppingList>> GetManyByUserAsync(long userId, CancellationToken ct = new());
}