using System.Security.Claims;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.Auth.UserInfo;

namespace Shopperior.WebApi.Shared.Services;

public class CurrentUserResolver : ICurrentUserResolver
{
    private readonly ILogger<CurrentUserResolver> _logger;
    private readonly IGetOneUserQuery _getOneUserQuery;
    private readonly IUserInfoResolver _userInfoResolver;
    private readonly IConfiguration _configuration;
    private readonly ICreateUserCommand _createUserCommand;

    public CurrentUserResolver(
        ILogger<CurrentUserResolver> logger,
        IGetOneUserQuery getOneUserQuery,
        IUserInfoResolver userInfoResolver,
        IConfiguration configuration,
        ICreateUserCommand createUserCommand)
    {
        _logger = logger;
        _getOneUserQuery = getOneUserQuery;
        _userInfoResolver = userInfoResolver;
        _configuration = configuration;
        _createUserCommand = createUserCommand;
    }

    public async Task<CurrentUser> Resolve(ClaimsPrincipal principal, string authorizationHeaderValue, CancellationToken cancellationToken = new())
    {
        if (principal.Identity == null) return null;

        var username = principal.Identity.Name;
        var user = await _getOneUserQuery.ExecuteAsync(username, cancellationToken);
        if (user != null)
        {
            var currentUser = Map(user);
            return currentUser;
        }

        var userInfo = await _userInfoResolver.Resolve(authorizationHeaderValue);
        if (userInfo == null) return null;

        var newUser = Map(userInfo);
        var response = await _createUserCommand.ExecuteAsync(newUser, cancellationToken);
        if (!response.IsSuccess)
        {
            _logger.LogError(string.Join('|', response.Messages));
            return null;
        }

        var newCurrentUser = Map(response.Data);

        return newCurrentUser;
    }

    private CurrentUser Map(User user)
    {
        var currentUser = new CurrentUser
        {
            Guid = user.Guid,
            Username = user.Username,
            GivenName = user.FirstName,
            FamilyName = user.LastName,
            EmailAddress = user.EmailAddress,
            Idp = user.Idp,
            IdpSubject = user.IdpSubject
        };

        return currentUser;
    }

    private User Map(UserInfo userInfo)
    {
        var user = new User
        {
            Username = userInfo.Subject,
            FirstName = userInfo.GivenName,
            LastName = userInfo.FamilyName,
            EmailAddress = userInfo.EmailAddress,
            Idp = _configuration.GetValue<string>("OIDC:Authority"),
            IdpSubject = userInfo.Subject
        };

        return user;
    }
}