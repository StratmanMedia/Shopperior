namespace Shopperior.WebApi.ShoppingLists.Models;

public class ListItemDto
{
    public Guid Guid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Comment { get; set; }
    public double Quantity { get; set; }
    public string Measurement { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsInCart { get; set; }
    public DateTime? EnteredCartTime { get; set; }
    public bool HasPurchased { get; set; }
    public DateTime? PurchasedTime { get; set; }
}