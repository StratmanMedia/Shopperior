using Shopperior.Domain.Contracts.Categories.Models;
using Shopperior.Domain.Contracts.ListItems.Models;

namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IShoppingListModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public IEnumerable<IUserListPermissionModel> Permissions { get; set; }
    public IEnumerable<ICategoryModel> Categories { get; set; }
    public IEnumerable<IListItemModel> Items { get; set; }
}