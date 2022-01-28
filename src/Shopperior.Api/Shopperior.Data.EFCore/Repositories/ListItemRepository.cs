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

        public async Task<ListItem> GetOne(long id)
        {
            return await Task.Run(() =>
            {
                var listItem = Table.FirstOrDefault(i => i.Id == id);

                return listItem;
            });
        }

        public async Task<ListItem> GetOne(Guid guid)
        {
            return await Task.Run(() =>
            {
                var listItem = Table.FirstOrDefault(i => i.Guid == guid);

                return listItem;
            });
        }
    }
}