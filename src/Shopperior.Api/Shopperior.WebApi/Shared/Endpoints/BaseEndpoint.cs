using Microsoft.AspNetCore.Mvc;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint : ControllerBase
{
    protected IActionResult InternalServerError()
    {
        return StatusCode(500);
    }

    protected string GetAuthorizationHeaderValue()
    {
        var authorizationHeaderValue = HttpContext.Request.Headers["Authorization"].ToString();

        return authorizationHeaderValue;
    }
}