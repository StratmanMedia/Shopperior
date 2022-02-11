namespace Shopperior.Domain.Contracts.Shared.Models;

public interface IDatabaseStatus
{
    string Status { get; set; }
    DateTime? Timestamp { get; set; }
}