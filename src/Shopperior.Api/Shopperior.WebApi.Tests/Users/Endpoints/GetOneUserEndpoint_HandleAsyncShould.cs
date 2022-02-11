using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;
using Shopperior.Domain.ValueObjects;
using Shopperior.Tests;
using Shopperior.WebApi.Users.Endpoints;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Tests.Users.Endpoints;

[TestClass]
public class GetOneUserEndpoint_HandleAsyncShould : BaseTest<GetOneUserEndpoint>
{
    [TestMethod]
    public async Task ReturnOkObjectResultWhenRequestContainsExistingEmailAddress()
    {
        var request = new GetOneUserRequest
        {
            Username = "",
            EmailAddress = "unittest@gmail.com"
        };
        var user = new User
        {
            Id = RandomLong(1),
            Guid = Guid.NewGuid(),
            Username = "unittestusername",
            FirstName = "Unit",
            LastName = "Test",
            EmailAddress = "unittest@gmail.com",
            Idp = "idp",
            IdpSubject = "idp|unittestusername",
            CreatedTime = DateTime.UtcNow
        };

        var sut = GetSystemUnderTest();
        Mock.Mock<IGetOneUserQuery>()
            .Setup(m => m.ExecuteAsync(It.IsAny<EmailAddress>(), new CancellationToken()))
            .ReturnsAsync(user);
        var response = await sut.HandleAsync(request);

        Assert.IsTrue(response.Result?.GetType() == typeof(OkObjectResult));
    }

    [TestMethod]
    public async Task ReturnNotFoundResultWhenRequestContainsUnfoundedEmailAddress()
    {
        var request = new GetOneUserRequest
        {
            Username = "",
            EmailAddress = "unittest@gmail.com"
        };

        var sut = GetSystemUnderTest();
        Mock.Mock<IGetOneUserQuery>()
            .Setup(m => m.ExecuteAsync(It.IsAny<EmailAddress>(), new CancellationToken()))
            .ReturnsAsync((User)null);
        var response = await sut.HandleAsync(request);

        Assert.IsTrue(response.Result?.GetType() == typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task ReturnBadRequestResultWhenRequestContainsEmptyEmailAddress()
    {
        var request = new GetOneUserRequest
        {
            Username = "",
            EmailAddress = ""
        };

        var sut = GetSystemUnderTest();
        Mock.Mock<IGetOneUserQuery>()
            .Setup(m => m.ExecuteAsync(It.IsAny<EmailAddress>(), new CancellationToken()))
            .ReturnsAsync((User)null);
        var response = await sut.HandleAsync(request);

        Assert.IsTrue(response.Result?.GetType() == typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task ReturnBadRequestResultWhenRequestIsNull()
    {
        var request = (GetOneUserRequest)null;

        var sut = GetSystemUnderTest();
        Mock.Mock<IGetOneUserQuery>()
            .Setup(m => m.ExecuteAsync(It.IsAny<EmailAddress>(), new CancellationToken()))
            .ReturnsAsync((User)null);
        var response = await sut.HandleAsync(request);

        Assert.IsTrue(response.Result?.GetType() == typeof(BadRequestObjectResult));
    }
}