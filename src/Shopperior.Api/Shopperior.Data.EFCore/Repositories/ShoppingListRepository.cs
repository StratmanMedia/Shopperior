using System.Collections.Generic;
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

    public Task<IEnumerable<ShoppingList>> GetAllByUserAsync()
    {
        throw new System.NotImplementedException();
    }
}