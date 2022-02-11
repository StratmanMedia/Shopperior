using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;
using Shopperior.Tests;
using Shopperior.WebApi.Shared.Services;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.Auth.UserInfo;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Tests.Shared.Services;

[TestClass]
public class CurrentUserResolver_ResolveShould : BaseTest<CurrentUserResolver>
{
    private readonly Mock<ILogger<CurrentUserResolver>> _logger = new Mock<ILogger<CurrentUserResolver>>();
    private readonly Mock<IGetOneUserQuery> _getOneUserQuery = new Mock<IGetOneUserQuery>();
    private readonly Mock<IUserInfoResolver> _userInfoResolver = new Mock<IUserInfoResolver>();
    private readonly IConfigurationRoot _configuration;
    private readonly Mock<ICreateUserCommand> _createUserCommand = new Mock<ICreateUserCommand>();
    private User _user;
    private UserInfo _userInfo;
    private Response<User> _responseUser;
    private HttpContext _httpContext;
    private CurrentUser _expectedCurrentUser;

    public CurrentUserResolver_ResolveShould()
    {
        var appSettings = new Dictionary<string, string> {
            {"OIDC:Authority", "idp"}
        };
        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(appSettings)
            .Build();
        _user = new User
        {
            Guid = Guid.NewGuid(),
            Username = "idp|unittestusername",
            FirstName = "Unit",
            LastName = "Test",
            EmailAddress = "unittest@gmail.com",
            Idp = "idp",
            IdpSubject = "idp|unittestusername"
        };
        _userInfo = new UserInfo
        {
            Subject = _user.IdpSubject,
            GivenName = _user.FirstName,
            FamilyName = _user.LastName,
            EmailAddress = _user.EmailAddress
        };
        _responseUser = new Response<User>(_user);
        var claims = new List<Claim>
        {
            new Claim("name", _user.Username)
        };
        var identity = new ClaimsIdentity(claims, "Bearer", "name", "role");
        _httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(identity)
        };
        _expectedCurrentUser = new CurrentUser
        {
            Guid = _user.Guid,
            Username = _user.Username,
            GivenName = _user.FirstName,
            FamilyName = _user.LastName,
            EmailAddress = _user.EmailAddress,
            Idp = _user.Idp,
            IdpSubject = _user.IdpSubject
        };
    }

    [TestMethod]
    public async Task ReturnExistingCurrentUserWhenUsernameExists()
    {
        _getOneUserQuery
            .Setup(m => m.ExecuteAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(_user);
        var sut = new CurrentUserResolver(_logger.Object, _getOneUserQuery.Object, _userInfoResolver.Object, _configuration, _createUserCommand.Object);
        var currentUser = await sut.Resolve(_httpContext, new CancellationToken());

        Assert.AreEqual(currentUser.Guid, _expectedCurrentUser.Guid);
        Assert.AreEqual(currentUser.Username, _expectedCurrentUser.Username);
        Assert.AreEqual(currentUser.GivenName, _expectedCurrentUser.GivenName);
        Assert.AreEqual(currentUser.FamilyName, _expectedCurrentUser.FamilyName);
        Assert.AreEqual(currentUser.EmailAddress, _expectedCurrentUser.EmailAddress);
        Assert.AreEqual(currentUser.Idp, _expectedCurrentUser.Idp);
        Assert.AreEqual(currentUser.IdpSubject, _expectedCurrentUser.IdpSubject);
    }

    [TestMethod]
    public async Task CreateNewUserAndReturnNewUserWhenUsernameNotFound()
    {
        _getOneUserQuery
            .Setup(m => m.ExecuteAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync((User)null);
        _userInfoResolver
            .Setup(m => m.Resolve(It.IsAny<string>()))
            .ReturnsAsync(_userInfo);
        _createUserCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<User>(), new CancellationToken()))
            .ReturnsAsync(_responseUser);
        var sut = new CurrentUserResolver(_logger.Object, _getOneUserQuery.Object, _userInfoResolver.Object, _configuration, _createUserCommand.Object);
        var currentUser = await sut.Resolve(_httpContext, new CancellationToken());

        Assert.AreEqual(currentUser.Guid, _expectedCurrentUser.Guid);
        Assert.AreEqual(currentUser.Username, _expectedCurrentUser.Username);
        Assert.AreEqual(currentUser.GivenName, _expectedCurrentUser.GivenName);
        Assert.AreEqual(currentUser.FamilyName, _expectedCurrentUser.FamilyName);
        Assert.AreEqual(currentUser.EmailAddress, _expectedCurrentUser.EmailAddress);
        Assert.AreEqual(currentUser.Idp, _expectedCurrentUser.Idp);
        Assert.AreEqual(currentUser.IdpSubject, _expectedCurrentUser.IdpSubject);
    }

    [TestMethod]
    public async Task ReturnNullWhenIdentityIsNull()
    {
        _httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal()
        };

        var sut = new CurrentUserResolver(_logger.Object, _getOneUserQuery.Object, _userInfoResolver.Object, _configuration, _createUserCommand.Object);
        var currentUser = await sut.Resolve(_httpContext, It.IsAny<CancellationToken>());

        Assert.IsNull(currentUser);
    }

    [TestMethod]
    public async Task ReturnNullWhenUserInfoIsNull()
    {
        _getOneUserQuery
            .Setup(m => m.ExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);
        _userInfoResolver
            .Setup(m => m.Resolve(It.IsAny<string>()))
            .ReturnsAsync((UserInfo)null);
        var sut = new CurrentUserResolver(_logger.Object, _getOneUserQuery.Object, _userInfoResolver.Object, _configuration, _createUserCommand.Object);
        var currentUser = await sut.Resolve(_httpContext, It.IsAny<CancellationToken>());

        Assert.IsNull(currentUser);
    }

    [TestMethod]
    public async Task ReturnNullWhenCreateUserFails()
    {
        _getOneUserQuery
            .Setup(m => m.ExecuteAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);
        _userInfoResolver
            .Setup(m => m.Resolve(It.IsAny<string>()))
            .ReturnsAsync(_userInfo);
        _createUserCommand
            .Setup(m => m.ExecuteAsync(It.IsAny<User>(), new CancellationToken()))
            .ReturnsAsync(new Response<User>("failed"));
        var sut = new CurrentUserResolver(_logger.Object, _getOneUserQuery.Object, _userInfoResolver.Object, _configuration, _createUserCommand.Object);
        var currentUser = await sut.Resolve(_httpContext, It.IsAny<CancellationToken>());

        Assert.IsNull(currentUser);
    }
}