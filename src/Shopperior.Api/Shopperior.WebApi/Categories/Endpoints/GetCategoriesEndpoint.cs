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
    private readonly IGetAllCategoriesByUserQuery _getAllCategoriesByUserQuery;
    private readonly ICategoryDtoResolver _categoryDtoResolver;

    public GetCategoriesEndpoint(
        ILogger<GetCategoriesEndpoint> logger,
        ICurrentUserProvider currentUserProvider,
        IGetAllCategoriesByUserQuery getAllCategoriesByUserQuery,
        ICategoryDtoResolver categoryDtoResolver) : base(logger)
    {
        _currentUserProvider = currentUserProvider;
        _getAllCategoriesByUserQuery = getAllCategoriesByUserQuery;
        _categoryDtoResolver = categoryDtoResolver;
    }

    [Authorize]
    [HttpGet("api/v1/categories")]
    public async Task<ActionResult<Response<CategoryDto[]>>> HandleAsync(CancellationToken ct = default)
    {
        return await TryActionAsync(() => EndpointAction(ct));
    }

    private async Task<CategoryDto[]> EndpointAction(CancellationToken ct = default)
    {
        var currentUser = _currentUserProvider.CurrentUser;
        if (currentUser == null)
            throw new UnauthorizedAccessException();

        var categories = await _getAllCategoriesByUserQuery.ExecuteAsync(currentUser.Guid, ct);
        var dtos = new List<CategoryDto>();
        foreach (var categoryModel in categories)
        {
            var dto = await _categoryDtoResolver.ResolveAsync(categoryModel, ct);
            dtos.Add(dto);
        }

        return dtos.ToArray();
    }
}