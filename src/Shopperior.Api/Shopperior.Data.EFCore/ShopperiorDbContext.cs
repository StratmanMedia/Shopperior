using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Entities;

namespace Shopperior.Data.EFCore
{
    public class ShopperiorDbContext : DbContext, IShopperiorDbContext
    {
        public ShopperiorDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ListItem> ListItems { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserListPermission> UserListPermissions { get; set; }
    }
}