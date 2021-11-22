using System;
using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Entities;

namespace Shopperior.Data.EFCore
{
    public interface IShopperiorDbContext : IDisposable
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<ListItem> ListItems { get; set; }
        DbSet<ShoppingList> ShoppingLists { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<User> Users { get; set; }
    }
}