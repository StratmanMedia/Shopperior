using System;
using System.Collections.Generic;

namespace Shopperior.Domain.Entities
{
    public class ShoppingList : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public ICollection<ListItem> Items { get; set; }
        public ICollection<UserListPermission> UserListPermissions { get; set; }
    }
}