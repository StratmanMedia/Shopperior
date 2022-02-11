using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.Shared.Queries;
using Shopperior.WebApi.Shared.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Shared.Endpoints;

public class CanaryEndpoint : BaseEndpoint<CanaryEndpoint>
{
    private readonly IDatabaseStatusQuery _databaseStatusQuery;

    public CanaryEndpoint(
        ILogger<CanaryEndpoint> logger,
        IDatabaseStatusQuery databaseStatusQuery) : base(logger)
    {
        _databaseStatusQuery = databaseStatusQuery;
    }

    [AllowAnonymous]
    [HttpGet("/api/v1/canary")]
    public async Task<ActionResult<Response<CanaryDto>>> HandleAsync(CancellationToken ct = default)
    {
        return await TryActionAsync<CanaryDto>(() => EndpointAction());
    }

    private async Task<CanaryDto> EndpointAction(CancellationToken ct = default)
    {
        var databaseStatus = await _databaseStatusQuery.ExecuteAsync(ct);
        var server = new CanaryStatusModel
        {
            Status = "OK",
            Timestamp = DateTime.Now
        };
        var database = new CanaryStatusModel
        {
            Status = databaseStatus.Status,
            Timestamp = databaseStatus.Timestamp
        };

        return new CanaryDto
        {
            Server = server,
            Database = database
        };
    }
}