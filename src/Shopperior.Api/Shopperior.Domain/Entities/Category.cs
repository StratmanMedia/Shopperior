using System;

namespace Shopperior.Domain.Entities
{
    public class Category : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}