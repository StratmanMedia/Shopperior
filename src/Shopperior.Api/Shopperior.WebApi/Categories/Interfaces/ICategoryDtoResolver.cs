using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.WebApi.Categories.Models;

namespace Shopperior.WebApi.Categories.Interfaces;

public interface ICategoryDtoResolver
{
    Task<CategoryDto> ResolveAsync(ICategoryModel model, CancellationToken ct = default);
    Task<ICategoryModel> ResolveAsync(CategoryDto dto, CancellationToken ct = default);
}