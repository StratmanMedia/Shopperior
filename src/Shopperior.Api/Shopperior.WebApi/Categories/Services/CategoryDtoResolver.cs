using Shopperior.Application.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.WebApi.Categories.Interfaces;
using Shopperior.WebApi.Categories.Models;

namespace Shopperior.WebApi.Categories.Services;

public class CategoryDtoResolver : ICategoryDtoResolver
{
    private readonly IGetOneShoppingListQuery _getOneShoppingListQuery;

    public CategoryDtoResolver(
        IGetOneShoppingListQuery getOneShoppingListQuery)
    {
        _getOneShoppingListQuery = getOneShoppingListQuery;
    }

    public async Task<CategoryDto> ResolveAsync(ICategoryModel model, CancellationToken ct = default)
    {
        if (model == null) return null;

        var dto = new CategoryDto
        {
            Guid = model.Guid,
            ShoppingListGuid = model.ShoppingListGuid,
            Name = model.Name
        };

        return await Task.FromResult(dto);
    }

    public async Task<ICategoryModel> ResolveAsync(CategoryDto dto, CancellationToken ct = default)
    {
        if (dto == null) return null;
        
        var model = new CategoryModel
        {
            Guid = dto.Guid,
            ShoppingListGuid = dto.ShoppingListGuid,
            Name = dto.Name
        };

        return await Task.FromResult(model);
    }
}