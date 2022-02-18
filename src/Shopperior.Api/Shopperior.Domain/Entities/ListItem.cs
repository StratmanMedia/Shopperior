namespace Shopperior.Domain.Entities
{
    public class ListItem
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public long ItemId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Comment { get; set; }
        public long StoreId { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public double Quantity { get; set; }
        public string Measurement { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsInCart { get; set; }
        public bool HasPurchased { get; set; }
        public DateTime? PurchasedTime { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? TrashedTime { get; set; }
    }
}