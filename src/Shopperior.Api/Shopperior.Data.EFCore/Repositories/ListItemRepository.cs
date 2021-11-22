using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories
{
    public class ListItemRepository : Repository<ShopperiorDbContext, ListItem>, IListItemRepository
    {
        public ListItemRepository(ShopperiorDbContext context) : base(context)
        {
        }
    }
}