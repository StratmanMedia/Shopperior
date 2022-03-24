using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Categories.Commands;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;

namespace Shopperior.Application.Categories.Commands
{
    public class CreateCategoryCommand : ICreateCategoryCommand
    {
        private readonly ILogger _logger;
        private readonly ICategoryModelResolver _categoryModelResolver;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommand(
            ILogger<CreateCategoryCommand> logger,
            ICategoryModelResolver categoryModelResolver,
            ICategoryRepository categoryRepository)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _categoryModelResolver = Guard.Against.Null(categoryModelResolver, nameof(categoryModelResolver));
            _categoryRepository = Guard.Against.Null(categoryRepository, nameof(categoryRepository));
        }

        public async Task ExecuteAsync(ICategoryModel request, CancellationToken ct = default)
        {
            await ValidateRequest(request, ct);
            var category = await _categoryModelResolver.ResolveAsync(request, ct);
            Guard.Against.Null(category, nameof(category));

            category.Guid = Guid.NewGuid();
            category.CreatedTime = DateTime.UtcNow;

            await _categoryRepository.CreateAsync(category, ct);
        }

        private Task ValidateRequest(ICategoryModel request, CancellationToken ct = default)
        {
            Guard.Against.Null(request, nameof(request));
            Guard.Against.Null(request.ShoppingListGuid, nameof(request.ShoppingListGuid));
            Guard.Against.NullOrWhiteSpace(request.Name, nameof(request.Name));

            return Task.CompletedTask;
        }
    }
}