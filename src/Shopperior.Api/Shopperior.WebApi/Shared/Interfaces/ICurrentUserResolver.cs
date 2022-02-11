using System.Security.Claims;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Shared.Interfaces;

public interface ICurrentUserResolver
{
    Task<CurrentUser> Resolve(HttpContext httpContext, CancellationToken cancellationToken);
}