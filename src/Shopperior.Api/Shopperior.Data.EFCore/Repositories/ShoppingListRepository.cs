using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class ShoppingListRepository : Repository<ShopperiorDbContext, ShoppingList>, IShoppingListRepository
{
    public ShoppingListRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<ShoppingList>> GetManyByUserAsync(long userId, CancellationToken ct = new())
    {
        //Table.Include(t => t.UserListPermissions);
        //var lists = Table.Where(l => l.UserListPermissions.Any(p => p.UserId == userId)).AsEnumerable();
        var lists = Table.AsEnumerable();

        return Task.FromResult(lists);
    }
}