using System;
using System.Collections.Generic;
using Shopperior.Domain.Enumerations;

namespace Shopperior.Domain.Entities
{
    public class User
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? TrashedTime { get; set; }
        public ICollection<UserListPermission> UserListPermissions => new HashSet<UserListPermission>();
    }
}