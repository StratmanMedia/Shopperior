using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IShoppingListRepository : IRepository<ShoppingList>
{
    Task<ShoppingList> GetOneAsync(long id, CancellationToken ct = default);
    Task<ShoppingList> GetOneAsync(Guid guid, CancellationToken ct = default);
    Task<ShoppingList[]> GetManyByUserAsync(long userId, CancellationToken ct = default);
}