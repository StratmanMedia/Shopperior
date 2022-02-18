using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.Categories.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetOneAsync(long id, CancellationToken ct = default);
        Task<Category> GetOneAsync(Guid guid, CancellationToken ct = default);
    }
}