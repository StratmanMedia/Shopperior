using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Categories;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Categories.Queries
{
    public class GetAllCategoriesQuery : IGetAllCategoriesQuery
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _categoryRepository;

        public GetAllCategoriesQuery(
            ILogger<GetAllCategoriesQuery> logger,
            ICategoryRepository categoryRepository)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _categoryRepository = Guard.Against.Null(categoryRepository, nameof(categoryRepository));
        }

        public async Task<IEnumerable<Category>> ExecuteAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();

                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(GetAllCategoriesQuery)}");
                throw;
            }
        }
    }
}