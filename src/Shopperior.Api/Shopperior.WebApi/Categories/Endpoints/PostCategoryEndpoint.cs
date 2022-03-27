using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.Categories.Commands;
using Shopperior.WebApi.Categories.Interfaces;
using Shopperior.WebApi.Categories.Models;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Shared.Interfaces;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Categories.Endpoints;

public class PostCategoryEndpoint : BaseEndpoint<PostCategoryEndpoint>
{
    private readonly ILogger<PostCategoryEndpoint> _logger;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly ICategoryDtoResolver _categoryDtoResolver;
    private readonly ICreateCategoryCommand _createCategoryCommand;

    public PostCategoryEndpoint(
        ILogger<PostCategoryEndpoint> logger,
        ICurrentUserProvider currentUserProvider,
        ICategoryDtoResolver categoryDtoResolver,
        ICreateCategoryCommand createCategoryCommand) : base(logger)
    {
        _logger = logger;
        _currentUserProvider = currentUserProvider;
        _categoryDtoResolver = categoryDtoResolver;
        _createCategoryCommand = createCategoryCommand;
    }

    [Authorize]
    [HttpPost("api/v1/categories")]
    public async Task<ActionResult<Response>> HandleAsync([FromBody] CategoryDto dto, CancellationToken ct = default)
    {
        return await TryActionAsync(() => EndpointAction(dto, ct));
    }

    private async Task EndpointAction(CategoryDto dto, CancellationToken ct = default)
    {
        var user = _currentUserProvider.CurrentUser;
        if (user == null)
            throw new UnauthorizedAccessException();

        await ValidateRequest(dto, ct);
        var model = await _categoryDtoResolver.ResolveAsync(dto, ct);
        await _createCategoryCommand.ExecuteAsync(model, ct);
    }

    private Task ValidateRequest(CategoryDto dto, CancellationToken ct = default)
    {
        if (dto == null)
            throw new BadHttpRequestException("The request body was malformed.");

        if (dto.ShoppingListGuid == Guid.Empty)
            throw new BadHttpRequestException($"The request body did not contain a valid {nameof(dto.ShoppingListGuid)}");

        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new BadHttpRequestException($"The request body did not contain {nameof(dto.Name)}.");

        return Task.CompletedTask;
    }
}