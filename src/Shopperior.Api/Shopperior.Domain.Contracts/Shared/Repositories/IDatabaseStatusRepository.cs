using Shopperior.Domain.Contracts.Shared.Models;

namespace Shopperior.Domain.Contracts.Shared.Repositories;

public interface IDatabaseStatusRepository
{
    Task<IDatabaseStatus> GetStatus();
}