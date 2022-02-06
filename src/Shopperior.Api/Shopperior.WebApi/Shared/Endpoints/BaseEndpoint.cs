using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint : ControllerBase
{
    private readonly ILogger<BaseEndpoint> _logger;

    protected BaseEndpoint()
    {
        
    }

    protected BaseEndpoint(ILogger<BaseEndpoint> logger)
    {
        _logger = logger;
    }

    protected async Task<ActionResult<Response>> TryActionAsync(Func<Task> func)
    {
        try
        {
            var sw = new Stopwatch();
            sw.Start();

            await func();
            sw.Stop();

            _logger.LogTrace($"Action completed in {sw.ElapsedMilliseconds}ms.");
            return Ok(new Response());
        }
        catch (BadHttpRequestException ex)
        {
            _logger.LogInformation(ex, ex.JoinAllMessages());
            return BadRequest(new Response(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return InternalServerError();
        }
    }

    protected async Task<T> GetRequestBody<T>()
    {
        using var reader = new StreamReader(Request.Body);
        var body = await reader.ReadToEndAsync();
        var obj = JsonConvert.DeserializeObject<T>(body);

        return obj;
    }

    protected ActionResult InternalServerError()
    {
        return StatusCode(500);
    }
}