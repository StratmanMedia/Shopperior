using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Enumerations;

namespace Shopperior.Application.ListItems.Models;

public class ListItemModel : IListItemModel
{
    public Guid Guid { get; set; }
    public IShoppingListModel ShoppingList { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Comment { get; set; }
    public double Quantity { get; set; }
    public Measurement Measurement { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsInCart { get; set; }
    public DateTime? EnteredCartTime { get; set; }
    public bool HasPurchased { get; set; }
    public DateTime? PurchasedTime { get; set; }
}