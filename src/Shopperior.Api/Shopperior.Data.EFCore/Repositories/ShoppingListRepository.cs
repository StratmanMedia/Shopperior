using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class ShoppingListRepository : Repository<ShopperiorDbContext, ShoppingList>, IShoppingListRepository
{
    public ShoppingListRepository(
        ShopperiorDbContext context) : base(context)
    {
    }

    public async Task<ShoppingList> GetOneAsync(long id, CancellationToken ct = default)
    {
        var shoppingList = Table.FirstOrDefault(l => l.Id == id);

        return await Task.FromResult(shoppingList);
    }

    public async Task<ShoppingList> GetOneAsync(Guid guid, CancellationToken ct = default)
    {
        var shoppingList = Table.FirstOrDefault(l => l.Guid == guid);

        return await Task.FromResult(shoppingList);
    }

    public async Task<ShoppingList[]> GetManyByUserAsync(long userId, CancellationToken ct = default)
    {
        var query = Table
            .Include(l => l.Permissions).ThenInclude(p => p.User)
            .Include(l => l.Categories)
            .Include(l => l.Items);
        var lists = query.Where(l => l.Permissions.Select(p => p.UserId).Contains(userId));

        return await Task.FromResult(lists.ToArray());
    }
}