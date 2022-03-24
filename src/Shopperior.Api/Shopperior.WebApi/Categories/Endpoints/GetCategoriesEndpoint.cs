using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.Categories.Queries;
using Shopperior.WebApi.Categories.Interfaces;
using Shopperior.WebApi.Categories.Models;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Categories.Endpoints;

public class GetCategoriesEndpoint : BaseEndpoint<GetCategoriesEndpoint>
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IGetAllCategoriesByShoppingListQuery _getAllCategoriesByShoppinglistQuery;
    private readonly ICategoryDtoResolver _categoryDtoResolver;

    public GetCategoriesEndpoint(
        ILogger<GetCategoriesEndpoint> logger,
        ICurrentUserProvider currentUserProvider,
        IGetAllCategoriesByShoppingListQuery getAllCategoriesByShoppinglistQuery,
        ICategoryDtoResolver categoryDtoResolver) : base(logger)
    {
        _currentUserProvider = currentUserProvider;
        _getAllCategoriesByShoppinglistQuery = getAllCategoriesByShoppinglistQuery;
        _categoryDtoResolver = categoryDtoResolver;
    }

    [Authorize]
    [HttpGet("api/v1/categories")]
    public async Task<ActionResult<Response<CategoryDto[]>>> HandleAsync(Guid shoppingList, CancellationToken ct = default)
    {
        return await TryActionAsync(() => EndpointAction(shoppingList, ct));
    }

    private async Task<CategoryDto[]> EndpointAction(Guid shoppingListGuid, CancellationToken ct = default)
    {
        var categories = await _getAllCategoriesByShoppinglistQuery.ExecuteAsync(shoppingListGuid, ct);
        var dtos = new List<CategoryDto>();
        foreach (var categoryModel in categories)
        {
            var dto = await _categoryDtoResolver.ResolveAsync(categoryModel, ct);
            dtos.Add(dto);
        }

        return dtos.ToArray();
    }
}