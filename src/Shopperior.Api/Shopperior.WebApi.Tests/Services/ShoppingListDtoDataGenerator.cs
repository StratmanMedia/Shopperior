using Shopperior.WebApi.ShoppingLists.Models;
using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.Tests.Services;

public class ShoppingListDtoDataGenerator
{
    private readonly RandomDataGenerator _random = new();
    private readonly ListPermissionDtoDataGenerator _permissionGenerator = new();
    private readonly ListItemDtoDataGenerator _itemGenerator = new();

    public ShoppingListDto Generate(CurrentUser currentUser = default)
    {
        var guid = Guid.NewGuid();
        currentUser = currentUser ?? new CurrentUser
        {
            Guid = Guid.NewGuid(),
            Username = _random.RandomString(16),
            GivenName = _random.RandomString(16),
            FamilyName = _random.RandomString(16),
            EmailAddress = _random.RandomString(16),
            Idp = _random.RandomString(16),
            IdpSubject = _random.RandomString(16)
        };
        var shoppingList = new ShoppingListDto
        {
            Guid = guid,
            Name = _random.RandomString(16),
            Permissions = _permissionGenerator.Generate(_random.RandomLong(1,3), guid, currentUser),
            Items = _itemGenerator.Generate(_random.RandomLong(0,20), guid)
        };

        return shoppingList;
    }
}