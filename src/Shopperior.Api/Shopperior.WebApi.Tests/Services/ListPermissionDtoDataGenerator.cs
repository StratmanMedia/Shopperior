using Shopperior.Domain.Enumerations;
using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Tests.Services;

public class ListPermissionDtoDataGenerator
{
    private readonly RandomDataGenerator _random = new();

    public IEnumerable<UserListPermissionDto> Generate(long numberToGenerate, Guid listGuid = default, CurrentUser currentUser = default)
    {
        var currentUserIsOwner = (numberToGenerate == 1 || _random.RandomBoolean()) && currentUser != default;
        var permissions = new List<UserListPermissionDto>
        {
            new UserListPermissionDto
            {
                User = (currentUserIsOwner) 
                    ? new UserDto
                    {
                        Guid = currentUser.Guid,
                        Username = currentUser.Username,
                        FirstName = currentUser.GivenName,
                        LastName = currentUser.FamilyName,
                        EmailAddress = currentUser.EmailAddress
                    }
                    : new UserDto
                    {
                        Guid = Guid.NewGuid(),
                        Username = _random.RandomString(16),
                        FirstName = _random.RandomString(16),
                        LastName = _random.RandomString(16),
                        EmailAddress = _random.RandomString(16)
                    },
                ShoppingListGuid = (listGuid == default) ? Guid.NewGuid() : listGuid,
                Permission = ShoppingListPermission.FromValue(3).ToString()
            }
        };
        for (var i = 0; i < numberToGenerate-1; i++)
        {
            var addCurrentUser = !currentUserIsOwner && permissions.Select(p => p.User.Guid).All(g => g != currentUser.Guid);
            var value = _random.RandomInt(0, 2);
            permissions.Add(new UserListPermissionDto
            {
                User = (addCurrentUser)
                    ? new UserDto
                    {
                        Guid = currentUser.Guid,
                        Username = currentUser.Username,
                        FirstName = currentUser.GivenName,
                        LastName = currentUser.FamilyName,
                        EmailAddress = currentUser.EmailAddress
                    }
                    : new UserDto
                    {
                        Guid = Guid.NewGuid(),
                        Username = _random.RandomString(16),
                        FirstName = _random.RandomString(16),
                        LastName = _random.RandomString(16),
                        EmailAddress = _random.RandomString(16)
                    },
                ShoppingListGuid = (listGuid == default) ? Guid.NewGuid() : listGuid,
                Permission = ShoppingListPermission.FromValue(value).ToString()
            });
        }

        return permissions;
    }
}