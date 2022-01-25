using Shopperior.WebApi.Users.Models;

namespace Shopperior.WebApi.ShoppingLists.Models;

public class UserListPermissionDto
{
    public UserDto User { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Permission { get; set; }
}