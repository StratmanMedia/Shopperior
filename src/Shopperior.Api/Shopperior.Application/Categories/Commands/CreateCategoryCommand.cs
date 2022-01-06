using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Categories;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Categories.Commands
{
    public class CreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ILogger _logger;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommand(
            ILogger<CreateCategoryCommand> logger,
            ICategoryRepository categoryRepository)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _categoryRepository = Guard.Against.Null(categoryRepository, nameof(categoryRepository));
        }

        public async Task ExecuteAsync(ICreateCategoryRequest request)
        {
            try
            {
                var category = new Category
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name
                };
                await _categoryRepository.CreateAsync(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(CreateCategoryCommand)}");
                throw;
            }
        }
    }
}