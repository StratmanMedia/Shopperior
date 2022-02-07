using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Shared.Interfaces;

public interface ICurrentUserProvider
{
    CurrentUser CurrentUser { get; }
    Task SetCurrentUser(CurrentUser currentUser);
}