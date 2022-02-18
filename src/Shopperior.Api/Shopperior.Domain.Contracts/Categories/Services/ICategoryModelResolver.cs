using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Categories.Services;

public interface ICategoryModelResolver
{
    Task<ICategoryModel> ResolveAsync(Category entity, CancellationToken ct = default);
    Task<Category> ResolveAsync(ICategoryModel model, CancellationToken ct = default);
}