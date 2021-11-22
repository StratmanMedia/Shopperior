using System;

namespace Shopperior.Domain.Entities
{
    public class Item : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Comment { get; set; }
    }
}