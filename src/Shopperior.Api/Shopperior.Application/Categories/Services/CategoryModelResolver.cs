using Shopperior.Application.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Categories.Services;

public class CategoryModelResolver : ICategoryModelResolver
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;

    public CategoryModelResolver(
        ICategoryRepository categoryRepository,
        IUserRepository userRepository)
    {
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
    }

    public async Task<ICategoryModel> ResolveAsync(Category entity, CancellationToken ct = default)
    {
        if (entity == null) return null;

        var model = new CategoryModel
        {
            Guid = entity.Guid,
            User = entity.User,
            Name = entity.Name
        };

        return await Task.FromResult(model);
    }

    public async Task<Category> ResolveAsync(ICategoryModel model, CancellationToken ct = default)
    {
        if (model == null) return null;

        var existing = await _categoryRepository.GetOneAsync(model.Guid, ct);
        if (existing == null)
        {
            var user = await _userRepository.GetAsync(model.User.Guid);
            var entity = new Category
            {
                Guid = model.Guid,
                User = user,
                Name = model.Name
            };

            return entity;
        }

        existing.Name = model.Name;

        return existing;
    }
}