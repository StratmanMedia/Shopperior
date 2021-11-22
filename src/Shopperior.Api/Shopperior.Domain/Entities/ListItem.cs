using System;
using Shopperior.Domain.Enumerations;

namespace Shopperior.Domain.Entities
{
    public class ListItem : BaseEntity
    {
        public Guid Guid { get; set; }
        public long ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public long ItemId { get; set; }
        public Item Item { get; set; }
        public long StoreId { get; set; }
        public Store Store { get; set; }
        public long AisleId { get; set; }
        public Category Category { get; set; }
        public double Quantity { get; set; }
        public Measurement Measurement { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsInCart { get; set; }
        public DateTime EnteredCartTime { get; set; }
        public bool HasPurchased { get; set; }
        public DateTime PurchasedTime { get; set; }
    }
}