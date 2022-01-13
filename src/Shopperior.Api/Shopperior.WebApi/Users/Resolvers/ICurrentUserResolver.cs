using System.Security.Claims;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Users.Resolvers;

public interface ICurrentUserResolver
{
    Task<CurrentUser?> Resolve(ClaimsPrincipal principal, string authorizationHeaderValue, CancellationToken cancellationToken);
}