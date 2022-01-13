namespace Shopperior.WebApi.Users.Models;

public class UserDto
{
    public Guid Guid { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
}