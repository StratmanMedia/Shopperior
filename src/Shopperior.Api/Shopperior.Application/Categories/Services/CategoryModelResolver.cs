using Shopperior.Application.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Categories.Services;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Categories.Services;

public class CategoryModelResolver : ICategoryModelResolver
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IShoppingListRepository _shoppingListRepository;

    public CategoryModelResolver(
        ICategoryRepository categoryRepository,
        IShoppingListRepository shoppingListRepository)
    {
        _categoryRepository = categoryRepository;
        _shoppingListRepository = shoppingListRepository;
    }

    public async Task<ICategoryModel> ResolveAsync(Category entity, CancellationToken ct = default)
    {
        if (entity == null) return null;

        var shoppingList = entity.ShoppingList ?? await _shoppingListRepository.GetOneAsync(entity.ShoppingListId, ct);

        var model = new CategoryModel
        {
            Guid = entity.Guid,
            ShoppingListGuid = shoppingList.Guid,
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
            var shoppingList = await _shoppingListRepository.GetOneAsync(model.ShoppingListGuid, ct);
            var entity = new Category
            {
                Guid = model.Guid,
                ShoppingListId = shoppingList.Id,
                ShoppingList = shoppingList,
                Name = model.Name
            };

            return entity;
        }

        existing.Name = model.Name;

        return existing;
    }
}