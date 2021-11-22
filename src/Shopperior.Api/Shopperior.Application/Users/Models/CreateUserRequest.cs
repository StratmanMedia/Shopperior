using Shopperior.Domain.Contracts.Users;

namespace Shopperior.Application.Users.Models
{
    public class CreateUserRequest : ICreateUserRequest
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}