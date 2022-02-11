using Shopperior.Domain.Contracts.Shared.Models;

namespace Shopperior.Domain.Contracts.Shared.Queries;

public interface IDatabaseStatusQuery
{
    Task<IDatabaseStatus> ExecuteAsync(CancellationToken ct = default);
}