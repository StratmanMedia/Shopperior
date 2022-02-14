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
            var listItem = Table.FirstOrDefault(i => i.Id == id);

            return await Task.FromResult(listItem);
        }

        public async Task<ListItem> GetOne(Guid guid)
        {
            var listItem = Table.FirstOrDefault(i => i.Guid == guid);

            return await Task.FromResult(listItem);
        }

        public async Task<ListItem[]> GetManyByListAsync(long shoppingListId)
        {
            var listItems = Table.Where(i => i.ShoppingListId == shoppingListId);

            return await Task.FromResult(listItems.ToArray());
        }
    }
}