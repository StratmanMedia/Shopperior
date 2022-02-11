using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Shared.Models;
using Shopperior.Domain.Contracts.Shared.Queries;
using Shopperior.Domain.Contracts.Shared.Repositories;

namespace Shopperior.Application.Shared.Queries;

public class DatabaseStatusQuery : IDatabaseStatusQuery
{
    private readonly ILogger<DatabaseStatusQuery> _logger;
    private readonly IDatabaseStatusRepository _databaseStatusRepository;

    public DatabaseStatusQuery(
        ILogger<DatabaseStatusQuery> logger,
        IDatabaseStatusRepository databaseStatusRepository)
    {
        _logger = logger;
        _databaseStatusRepository = databaseStatusRepository;
    }

    public async Task<IDatabaseStatus> ExecuteAsync(CancellationToken ct = default)
    {
        var status = await _databaseStatusRepository.GetStatus();

        return status;
    }
}