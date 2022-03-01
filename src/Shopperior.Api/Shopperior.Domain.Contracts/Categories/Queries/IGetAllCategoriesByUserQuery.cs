using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Categories.Queries
{
    public interface IGetAllCategoriesByUserQuery
    {
        Task<IEnumerable<ICategoryModel>> ExecuteAsync(Guid userGuid, CancellationToken ct = default);
    }
}