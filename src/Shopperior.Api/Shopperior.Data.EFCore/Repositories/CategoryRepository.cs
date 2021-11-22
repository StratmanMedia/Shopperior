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
    }
}