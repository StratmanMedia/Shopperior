using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Shared.Interfaces;

public interface ICurrentUserService
{
    CurrentUser CurrentUser { get; }
    Task SetCurrentUser(CurrentUser currentUser);
}