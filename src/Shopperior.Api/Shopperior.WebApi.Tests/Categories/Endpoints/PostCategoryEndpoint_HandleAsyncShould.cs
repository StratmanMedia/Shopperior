using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopperior.Domain.Contracts.Categories.Commands;
using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Tests;
using Shopperior.WebApi.Categories.Endpoints;
using Shopperior.WebApi.Categories.Interfaces;
using Shopperior.WebApi.Categories.Models;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Tests.Categories.Endpoints;

[TestClass]
public class PostCategoryEndpoint_HandleAsyncShould : BaseTest<PostCategoryEndpoint>
{
    private readonly Mock<ILogger<PostCategoryEndpoint>> _logger = new Mock<ILogger<PostCategoryEndpoint>>();
    private readonly Mock<ICurrentUserProvider> _currentUserProvider = new Mock<ICurrentUserProvider>();
    private readonly Mock<ICategoryDtoResolver> _categoryDtoResolver = new Mock<ICategoryDtoResolver>();
    private readonly Mock<ICreateCategoryCommand> _createCategoryCommand = new Mock<ICreateCategoryCommand>();
    private CategoryDto _request;
    private CurrentUser _currentUser;

    public PostCategoryEndpoint_HandleAsyncShould()
    {
        _request = new CategoryDto
        {
            Name = RandomString(16)
        };
        _currentUser = new CurrentUser
        {
            Guid = Guid.NewGuid(),
            Username = RandomString(16),
            GivenName = RandomString(16),
            FamilyName = RandomString(16),
            EmailAddress = RandomString(16),
            Idp = RandomString(16),
            IdpSubject = RandomString(16)
        };
    }

    [TestMethod]
    public async Task ReturnOkWhenHappyPath()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _categoryDtoResolver
            .Setup(m => m.ResolveAsync(It.IsAny<CategoryDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<ICategoryModel>());
        _createCategoryCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<ICategoryModel>(), It.IsAny<CancellationToken>()));
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);
        
        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var okObjectResult = actionResult.Result.Should().BeOfType<OkObjectResult>();
        var response = okObjectResult.Subject.Value.Should().BeOfType<Response>();
        response.Subject.IsSuccess.Should().BeTrue();
    }

    [TestMethod]
    public async Task ReturnUnauthorizedWhenCurrentUserIsNull()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns((CurrentUser)null);
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var unauthorizedResult = actionResult.Result.Should().BeOfType<UnauthorizedResult>();
    }

    [TestMethod]
    public async Task ReturnBadRequestWhenRequestBodyIsNull()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync((CategoryDto)null, It.IsAny<CancellationToken>());

        var badRequestObjectResult = actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        var response = badRequestObjectResult.Subject.Value.Should().BeOfType<Response>();
        response.Subject.IsSuccess.Should().BeFalse();
        response.Subject.Messages.Should().Contain("The request body was malformed.");
    }

    [TestMethod]
    public async Task ReturnBadRequestWhenNameIsNullInRequest()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _request.Name = null;
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var badRequestObjectResult = actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        var response = badRequestObjectResult.Subject.Value.Should().BeOfType<Response>();
        response.Subject.IsSuccess.Should().BeFalse();
        response.Subject.Messages.Should().Contain($"The request body did not contain {nameof(_request.Name)}.");
    }

    [TestMethod]
    public async Task ReturnBadRequestWhenNameIsEmptyInRequest()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _request.Name = string.Empty;
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var badRequestObjectResult = actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        var response = badRequestObjectResult.Subject.Value.Should().BeOfType<Response>();
        response.Subject.IsSuccess.Should().BeFalse();
        response.Subject.Messages.Should().Contain($"The request body did not contain {nameof(_request.Name)}.");
    }

    [TestMethod]
    public async Task ReturnBadRequestWhenNameIsWhitespaceInRequest()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _request.Name = "               ";
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var badRequestObjectResult = actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        var response = badRequestObjectResult.Subject.Value.Should().BeOfType<Response>();
        response.Subject.IsSuccess.Should().BeFalse();
        response.Subject.Messages.Should().Contain($"The request body did not contain {nameof(_request.Name)}.");
    }

    [TestMethod]
    public async Task Return500WhenCommandThrowsArgumentNullException()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _categoryDtoResolver
            .Setup(m => m.ResolveAsync(It.IsAny<CategoryDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<ICategoryModel>());
        _createCategoryCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<ICategoryModel>(), It.IsAny<CancellationToken>()))
            .Throws<ArgumentNullException>();
        var sut = new PostCategoryEndpoint(_logger.Object, _currentUserProvider.Object, _categoryDtoResolver.Object, _createCategoryCommand.Object);

        var actionResult = await sut.HandleAsync(_request, It.IsAny<CancellationToken>());

        var statusCodeResult = actionResult.Result.Should().BeOfType<StatusCodeResult>();
        var response = statusCodeResult.Subject.StatusCode.Should().Be(500);
    }
}