namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IShoppingListModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public IEnumerable<IUserListPermissionModel> Permissions { get; set; }
}