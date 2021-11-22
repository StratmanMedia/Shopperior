using System;

namespace Shopperior.Application.Shared.Models
{
    public class BaseModel
    {
        public Guid Guid { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public DateTime TrashedTime { get; set; }
    }
}