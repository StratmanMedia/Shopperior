using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Tests;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.ShoppingLists.Endpoints;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Tests.ShoppingLists.Endpoints;

[TestClass]
public class DeleteShoppingListEndpoint_HandleAsyncShould : BaseTest<DeleteShoppingListEndpoint>
{
    private readonly Mock<ILogger<DeleteShoppingListEndpoint>> _logger = new Mock<ILogger<DeleteShoppingListEndpoint>>();
    private readonly Mock<ICurrentUserProvider> _currentUserProvider = new Mock<ICurrentUserProvider>();
    private readonly Mock<IDeleteShoppingListCommand> _deleteShoppingListCommand = new Mock<IDeleteShoppingListCommand>();
    private Guid _guid;
    private CurrentUser _currentUser;

    public DeleteShoppingListEndpoint_HandleAsyncShould()
    {
        _guid = Guid.NewGuid();
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
        _deleteShoppingListCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<DeleteShoppingListRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response());
        var sut = new DeleteShoppingListEndpoint(_logger.Object, _currentUserProvider.Object, _deleteShoppingListCommand.Object);

        var actionResult = await sut.HandleAsync(_guid, It.IsAny<CancellationToken>());

        Assert.AreEqual(actionResult.Result?.GetType(), typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task ReturnBadRequestWhenGuidIsEmpty()
    {
        var sut = new DeleteShoppingListEndpoint(_logger.Object, _currentUserProvider.Object, _deleteShoppingListCommand.Object);

        var actionResult = await sut.HandleAsync(Guid.Empty, It.IsAny<CancellationToken>());

        Assert.AreEqual(actionResult.Result?.GetType(), typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task ReturnUnauthorizedWhenCurrentUserIsNull()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns((CurrentUser)null);
        var sut = new DeleteShoppingListEndpoint(_logger.Object, _currentUserProvider.Object, _deleteShoppingListCommand.Object);

        var actionResult = await sut.HandleAsync(_guid, It.IsAny<CancellationToken>());

        Assert.AreEqual(actionResult.Result?.GetType(), typeof(UnauthorizedResult));
    }

    [TestMethod]
    public async Task ReturnInternalServerErrorWhenDeleteCommandFails()
    {
        _currentUserProvider
            .Setup(m => m.CurrentUser)
            .Returns(_currentUser);
        _deleteShoppingListCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<DeleteShoppingListRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response("failed"));
        var sut = new DeleteShoppingListEndpoint(_logger.Object, _currentUserProvider.Object, _deleteShoppingListCommand.Object);

        var actionResult = await sut.HandleAsync(_guid, It.IsAny<CancellationToken>());

        Assert.AreEqual(actionResult.Result?.GetType(), typeof(StatusCodeResult));
        Assert.AreEqual((actionResult.Result as StatusCodeResult).StatusCode, (int)HttpStatusCode.InternalServerError);
    }
}