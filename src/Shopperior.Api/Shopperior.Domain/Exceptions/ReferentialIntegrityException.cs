namespace Shopperior.Domain.Exceptions;

public class ReferentialIntegrityException : Exception
{
    public ReferentialIntegrityException(string message): base(message)
    {
    }
}