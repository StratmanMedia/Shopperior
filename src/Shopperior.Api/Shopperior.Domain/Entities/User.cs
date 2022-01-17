using System;

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
        public string Idp { get; set; }
        public string IdpSubject { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? LastModifiedTime { get; set; }
        public DateTime? TrashedTime { get; set; }
    }
}