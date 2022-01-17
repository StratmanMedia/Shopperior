using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Entities;

namespace Shopperior.Data.EFCore
{
    public interface IShopperiorDbContext : IDisposable
    {
        DbSet<Category> Category { get; set; }
        DbSet<Item> Item { get; set; }
        DbSet<ListItem> ListItem { get; set; }
        DbSet<ShoppingList> ShoppingList { get; set; }
        DbSet<Store> Store { get; set; }
        DbSet<User> User { get; set; }
    }
}