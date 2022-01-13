namespace Shopperior.WebApi.Users.Models;

public class CurrentUser
{
    public string? Idp { get; set; }
    public string? IdpSubject { get; set; }
    public Guid Guid { get; set; }
    public string? Username { get; set; }
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? EmailAddress { get; set; }
}