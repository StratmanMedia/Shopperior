using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Shared.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUser CurrentUser { get; private set; }

    public async Task SetCurrentUser(CurrentUser currentUser)
    {
        await Task.Run(() =>
        {
            CurrentUser = currentUser;
        });
    }
}