using Shopperior.WebApi.Shared.Interfaces;

namespace Shopperior.WebApi.Shared.Middleware;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;

    public CurrentUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUserResolver currentUserResolver, ICurrentUserProvider currentUserService)
    {
        var currentUser = await currentUserResolver.Resolve(context, new CancellationToken());
        currentUserService.SetCurrentUser(currentUser);
        await _next(context);
    }
}

public static class CurrentUserMiddlewareExtensions
{
    public static IApplicationBuilder UseCurrentUser(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CurrentUserMiddleware>();
    }
}