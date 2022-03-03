using Shopperior.Domain.Contracts.Categories.Models;

namespace Shopperior.Domain.Contracts.Categories.Queries;

public interface IGetOneCategoryQuery
{
    Task<ICategoryModel> ExecuteAsync(Guid guid, CancellationToken ct = default);
}