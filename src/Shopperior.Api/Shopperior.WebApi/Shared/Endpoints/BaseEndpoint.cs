using Microsoft.AspNetCore.Mvc;

namespace Shopperior.WebApi.Shared.Endpoints;

public class BaseEndpoint : ControllerBase
{
    public IActionResult InternalServerError()
    {
        return StatusCode(500);
    }
}