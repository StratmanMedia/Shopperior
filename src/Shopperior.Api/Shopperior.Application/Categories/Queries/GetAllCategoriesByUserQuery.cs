using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Queries;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;

namespace Shopperior.Application.Categories.Queries
{
    public class GetAllCategoriesByUserQuery : IGetAllCategoriesByUserQuery
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryModelResolver _categoryModelResolver;

        public GetAllCategoriesByUserQuery(
            ILogger<GetAllCategoriesByUserQuery> logger,
            ICategoryRepository categoryRepository,
            ICategoryModelResolver categoryModelResolver)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _categoryRepository = Guard.Against.Null(categoryRepository, nameof(categoryRepository));
            _categoryModelResolver = Guard.Against.Null(categoryModelResolver, nameof(categoryModelResolver));
        }

        public async Task<IEnumerable<ICategoryModel>> ExecuteAsync(Guid userGuid, CancellationToken ct = default)
        {
            var entities = await _categoryRepository.GetManyByUser(userGuid, ct);
            var categories = new List<ICategoryModel>();
            foreach (var entity in entities)
            {
                var category = await _categoryModelResolver.ResolveAsync(entity, ct);
                if (category == null) continue;

                categories.Add(category);
            }

            return categories;
        }
    }
}