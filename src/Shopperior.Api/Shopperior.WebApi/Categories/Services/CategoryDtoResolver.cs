using Shopperior.Application.Categories.Models;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.Users;
using Shopperior.WebApi.Categories.Interfaces;
using Shopperior.WebApi.Categories.Models;

namespace Shopperior.WebApi.Categories.Services;

public class CategoryDtoResolver : ICategoryDtoResolver
{
    private readonly IGetOneUserQuery _getOneUserQuery;

    public CategoryDtoResolver(
        IGetOneUserQuery getOneUserQuery)
    {
        _getOneUserQuery = getOneUserQuery;
    }

    public async Task<CategoryDto> ResolveAsync(ICategoryModel model, CancellationToken ct = default)
    {
        if (model == null) return null;

        var dto = new CategoryDto
        {
            Guid = model.Guid,
            UserGuid = model.User.Guid,
            Name = model.Name
        };

        return await Task.FromResult(dto);
    }

    public async Task<ICategoryModel> ResolveAsync(CategoryDto dto, CancellationToken ct = default)
    {
        if (dto == null) return null;

        var user = await _getOneUserQuery.ExecuteAsync(dto.UserGuid, ct);
        var model = new CategoryModel
        {
            Guid = dto.Guid,
            User = user,
            Name = dto.Name
        };

        return await Task.FromResult(model);
    }
}