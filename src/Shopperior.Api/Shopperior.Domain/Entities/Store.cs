using System;

namespace Shopperior.Domain.Entities
{
    public class Store
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? TrashedTime { get; set; }
    }
}