using Microsoft.AspNetCore.Mvc;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint : ControllerBase
{
    protected ActionResult InternalServerError()
    {
        return StatusCode(500);
    }
}