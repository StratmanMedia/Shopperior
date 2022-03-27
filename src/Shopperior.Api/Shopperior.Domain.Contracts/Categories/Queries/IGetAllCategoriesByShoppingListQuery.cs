using Shopperior.Domain.Contracts.Categories.Models;

namespace Shopperior.Domain.Contracts.Categories.Queries
{
    public interface IGetAllCategoriesByShoppingListQuery
    {
        Task<IEnumerable<ICategoryModel>> ExecuteAsync(Guid shoppingListGuid, CancellationToken ct = default);
    }
}