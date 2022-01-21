using Shopperior.Domain.Contracts.ShoppingLists.Models;

namespace Shopperior.Application.ShoppingLists.Models;

public class UserListPermissionModel : IUserListPermissionModel
{
    public Guid UserGuid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Permission { get; set; }
}