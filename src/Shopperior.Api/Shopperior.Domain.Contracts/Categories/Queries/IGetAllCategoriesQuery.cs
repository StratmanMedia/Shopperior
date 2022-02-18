using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Categories.Queries
{
    public interface IGetAllCategoriesQuery
    {
        Task<IEnumerable<Category>> ExecuteAsync();
    }
}