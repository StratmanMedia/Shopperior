using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopperior.Domain.Exceptions;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint<TEndpoint> : ControllerBase
{
    private readonly ILogger<TEndpoint> _logger;

    protected BaseEndpoint(ILogger<TEndpoint> logger)
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
        catch (ResourceNotFoundException ex)
        {
            _logger.LogInformation(ex, ex.JoinAllMessages());
            return NotFound(new Response(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.JoinAllMessages());
            return InternalServerError();
        }
    }

    protected async Task<ActionResult<Response<TResult>>> TryActionAsync<TResult>(Func<Task<TResult>> func)
    {
        try
        {
            var sw = new Stopwatch();
            sw.Start();

            var result = await func();
            sw.Stop();

            _logger.LogTrace($"Action completed in {sw.ElapsedMilliseconds}ms.");
            return Ok(new Response<TResult>(result));
        }
        catch (BadHttpRequestException ex)
        {
            _logger.LogInformation(ex, ex.JoinAllMessages());
            return BadRequest(new Response(ex.Message));
        }
        catch (ResourceNotFoundException ex)
        {
            _logger.LogInformation(ex, ex.JoinAllMessages());
            return NotFound(new Response(ex.Message));
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

    //private void HandleAction(Func<>)
    //{
    //    try
    //    {
    //        var sw = new Stopwatch();
    //        sw.Start();

    //        var result = await func();
    //        sw.Stop();

    //        _logger.LogTrace($"Action completed in {sw.ElapsedMilliseconds}ms.");
    //        return Ok(new Response<TResult>(result));
    //    }
    //    catch (BadHttpRequestException ex)
    //    {
    //        _logger.LogInformation(ex, ex.JoinAllMessages());
    //        return BadRequest(new Response(ex.Message));
    //    }
    //    catch (Exception ex)
    //    {
    //        _logger.LogError(ex, ex.JoinAllMessages());
    //        return InternalServerError();
    //    }
    //}
}

public class BaseEndpoint : ControllerBase
{
    public BaseEndpoint()
    {

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