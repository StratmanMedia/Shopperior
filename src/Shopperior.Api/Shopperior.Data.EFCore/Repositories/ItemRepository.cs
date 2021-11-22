using Shopperior.Domain.Contracts.Items.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories
{
    public class ItemRepository : Repository<ShopperiorDbContext, Item>, IItemRepository
    {
        public ItemRepository(ShopperiorDbContext context) : base(context)
        {
        }
    }
}