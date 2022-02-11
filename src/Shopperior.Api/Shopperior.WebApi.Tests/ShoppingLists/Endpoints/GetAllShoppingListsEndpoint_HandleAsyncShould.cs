using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Tests;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Endpoints;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.Tests.Services;
using Shopperior.WebApi.Users.Interfaces;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Tests.ShoppingLists.Endpoints;

[TestClass]
public class GetAllShoppingListsEndpoint_HandleAsyncShould : BaseTest<GetAllShoppingListsEndpoint>
{
    private readonly GetAllShoppingListsEndpoint _sut;
    private readonly Mock<ILogger<GetAllShoppingListsEndpoint>> _logger = new();
    private readonly Mock<ICurrentUserProvider> _currentUserService = new();
    private readonly Mock<IGetAllShoppingListsQuery> _getAllShoppingListsQuery = new();
    private readonly Mock<IShoppingListDtoResolver> _shoppingListDtoResolver = new();
    private readonly RandomDataGenerator _random = new();
    private readonly ShoppingListDtoDataGenerator _listGenerator = new();
    private CurrentUser _currentUser;
    private IShoppingListModel[] _shoppingListModels;

    public GetAllShoppingListsEndpoint_HandleAsyncShould()
    {
        _sut = new GetAllShoppingListsEndpoint(
            _logger.Object, 
            _currentUserService.Object, 
            _getAllShoppingListsQuery.Object, 
            _shoppingListDtoResolver.Object);
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
        _shoppingListModels = new IShoppingListModel[RandomInt(0, 20)];
    }

    [TestMethod]
    public async Task ReturnOkWhenHappyPath()
    {
        _currentUserService
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _getAllShoppingListsQuery
            .Setup(m => m.ExecuteAsync(_currentUser.Guid, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_shoppingListModels);
        _shoppingListDtoResolver
            .Setup(m => m.ResolveAsync(It.IsAny<IShoppingListModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_listGenerator.Generate(_currentUser));

        var actionResult = await _sut.HandleAsync(It.IsAny<CancellationToken>());

        var okObjectResult = actionResult.Result.Should().BeOfType<OkObjectResult>();
        var response = okObjectResult.Subject.Value.Should().BeOfType<Response<ShoppingListDto[]>>();
        response.Subject.Data.Length.Should().Be(_shoppingListModels.Length);
    }

    [TestMethod]
    public async Task ReturnUnauthorizedWhenCurrentUserIsNull()
    {
        _currentUserService
            .Setup(m => m.CurrentUser)
            .Returns((CurrentUser)null);

        var actionResult = await _sut.HandleAsync(It.IsAny<CancellationToken>());

        var okObjectResult = actionResult.Result.Should().BeOfType<UnauthorizedResult>();
    }
}