using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint : ControllerBase
{
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