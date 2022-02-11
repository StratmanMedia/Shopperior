namespace Shopperior.WebApi.Shared.Models;

public class CanaryDto
{
    public CanaryStatusModel Server { get; set; }
    public CanaryStatusModel Database { get; set; }
}