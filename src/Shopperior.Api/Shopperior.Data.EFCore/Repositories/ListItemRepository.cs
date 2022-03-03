using Microsoft.EntityFrameworkCore;
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
            var query = Table
                .Include(i => i.ShoppingList)
                .Include(i => i.Category);
            var listItem = query.FirstOrDefault(i => i.Id == id);

            return await Task.FromResult(listItem);
        }

        public async Task<ListItem> GetOne(Guid guid)
        {
            var query = Table
                .Include(i => i.ShoppingList)
                .Include(i => i.Category);
            var listItem = query.FirstOrDefault(i => i.Guid == guid);

            return await Task.FromResult(listItem);
        }

        public async Task<ListItem[]> GetManyByListAsync(long shoppingListId)
        {
            var query = Table
                .Include(i => i.ShoppingList)
                .Include(i => i.Category);
            var listItems = query.Where(i => i.ShoppingListId == shoppingListId);

            return await Task.FromResult(listItems.ToArray());
        }
    }
}