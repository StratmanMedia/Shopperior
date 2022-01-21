using Shopperior.Domain.Contracts.ShoppingLists.Models;

namespace Shopperior.Application.ShoppingLists.Models;

public class ShoppingListModel : IShoppingListModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public IEnumerable<IUserListPermissionModel> Permissions { get; set; }
}