using Shopperior.Domain.Entities;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Users.Interfaces;

public interface IUserDtoResolver
{
    UserDto Resolve(User entity);
}