using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Entities;

namespace Shopperior.Data.EFCore
{
    public class ShopperiorDbContext : DbContext, IShopperiorDbContext
    {
        public ShopperiorDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ListItem> ListItem { get; set; }
        public DbSet<ShoppingList> ShoppingList { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserListPermission> UserListPermission { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}