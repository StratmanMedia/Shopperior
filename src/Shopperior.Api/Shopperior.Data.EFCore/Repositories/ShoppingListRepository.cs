using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class ShoppingListRepository : Repository<ShopperiorDbContext, ShoppingList>, IShoppingListRepository
{
    public ShoppingListRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ShoppingList>> GetManyByUserAsync(string username, CancellationToken cancellationToken = new())
    {
        return await Task.Run(() =>
        {
            var lists = Context.ShoppingLists.Where(l => l.UserListPermissions.Any(p => p.User.Username == username)).AsEnumerable();

            return lists;
        }, cancellationToken);
    }
}