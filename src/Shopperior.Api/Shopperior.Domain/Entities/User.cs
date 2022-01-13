using System;
using System.Collections.Generic;

namespace Shopperior.Domain.Entities
{
    public class User : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Idp { get; set; }
        public string IdpSubject { get; set; }
        public ICollection<UserListPermission> UserListPermissions => new HashSet<UserListPermission>();
    }
}