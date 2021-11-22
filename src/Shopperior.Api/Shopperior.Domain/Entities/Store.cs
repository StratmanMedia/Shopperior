using System;
using System.Collections.Generic;

namespace Shopperior.Domain.Entities
{
    public class Store : BaseEntity
    {
        public Guid Guid { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Item> Items { get; set; }
 
        public Store()
        {
            Categories = new HashSet<Category>();
            Items = new HashSet<Item>();
        }
    }
}