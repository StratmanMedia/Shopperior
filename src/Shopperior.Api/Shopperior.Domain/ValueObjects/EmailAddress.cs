namespace Shopperior.Domain.ValueObjects;

public class EmailAddress
{
    private readonly string _address;

    public EmailAddress(string emailAddress)
    {
        _address = emailAddress;
    }

    public string Value => _address;
}