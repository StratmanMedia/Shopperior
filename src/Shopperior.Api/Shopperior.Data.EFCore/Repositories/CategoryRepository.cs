using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories
{
    public class CategoryRepository : Repository<ShopperiorDbContext, Category>, ICategoryRepository
    {
        public CategoryRepository(ShopperiorDbContext context) : base(context)
        {
        }

        public async Task<Category> GetOneAsync(long id, CancellationToken ct = default)
        {
            var category = Table.FirstOrDefault(c => c.Id == id);

            return await Task.FromResult(category);
        }

        public async Task<Category> GetOneAsync(Guid guid, CancellationToken ct = default)
        {
            var category = Table.FirstOrDefault(c => c.Guid == guid);

            return await Task.FromResult(category);
        }

        public async Task<Category[]> GetManyByShoppingList(Guid guid, CancellationToken ct = default)
        {
            var categories = Table.Where(c => c.ShoppingList.Guid == guid);

            return await Task.FromResult(categories.ToArray());
        }
    }
}