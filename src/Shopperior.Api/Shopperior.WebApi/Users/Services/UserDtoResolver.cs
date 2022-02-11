using Shopperior.Domain.Entities;
using Shopperior.WebApi.Users.Interfaces;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Users.Services;

public class UserDtoResolver : IUserDtoResolver
{
    public UserDto Resolve(User entity)
    {
        var dto = new UserDto
        {
            Guid = entity.Guid,
            Username = entity.Username,
            EmailAddress = entity.EmailAddress,
            FirstName = entity.FirstName,
            LastName = entity.LastName
        };

        return dto;
    }
}