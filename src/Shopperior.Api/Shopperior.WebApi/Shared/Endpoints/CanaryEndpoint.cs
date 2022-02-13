using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopperior.Domain.Contracts.Shared.Queries;
using Shopperior.WebApi.Shared.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Shared.Endpoints;

public class CanaryEndpoint : BaseEndpoint<CanaryEndpoint>
{
    private readonly ILogger<CanaryEndpoint> _logger;
    private readonly IDatabaseStatusQuery _databaseStatusQuery;

    public CanaryEndpoint(
        ILogger<CanaryEndpoint> logger,
        IDatabaseStatusQuery databaseStatusQuery) : base(logger)
    {
        _logger = logger;
        _databaseStatusQuery = databaseStatusQuery;
    }

    [AllowAnonymous]
    [HttpGet("/api/v1/canary")]
    public async Task<ActionResult<Response<CanaryDto>>> HandleAsync(CancellationToken ct = default)
    {
        _logger.LogInformation("Canary Endpoint was called.");
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

        var result = new CanaryDto
        {
            Server = server,
            Database = database
        };
        _logger.LogInformation($"Canary Endpoint response: {JsonConvert.SerializeObject(result)}");

        return result;
    }
}