using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Models;

public class UserListPermissionModel : IUserListPermissionModel
{
    public User User { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public IShoppingListModel ShoppingList { get; set; }
    public string Permission { get; set; }
}