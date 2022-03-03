using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Queries;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;

namespace Shopperior.Application.Categories.Queries;

public class GetOneCategoryQuery : IGetOneCategoryQuery
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryModelResolver _categoryModelResolver;

    public GetOneCategoryQuery(
        ICategoryRepository categoryRepository,
        ICategoryModelResolver categoryModelResolver)
    {
        _categoryRepository = categoryRepository;
        _categoryModelResolver = categoryModelResolver;
    }

    public async Task<ICategoryModel> ExecuteAsync(Guid guid, CancellationToken ct = default)
    {
        var category = await _categoryRepository.GetOneAsync(guid, ct);
        var model = await _categoryModelResolver.ResolveAsync(category, ct);

        return model;
    }
}