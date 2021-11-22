using Shopperior.Domain.Contracts.Stores.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories
{
    public class StoreRepository : Repository<ShopperiorDbContext, Store>, IStoreRepository
    {
        public StoreRepository(ShopperiorDbContext context) : base(context)
        {
        }
    }
}