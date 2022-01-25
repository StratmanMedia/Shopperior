using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.ValueObjects;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Users.Endpoints;

public class GetOneUserEndpoint : BaseEndpoint
{
    private readonly IGetOneUserQuery _getOneUserQuery;

    public GetOneUserEndpoint(
        IGetOneUserQuery getOneUserQuery)
    {
        _getOneUserQuery = getOneUserQuery;
    }

    [Authorize]
    [HttpGet("/api/v1/users")]
    public async Task<ActionResult<Response<UserDto>>> HandleAsync(GetOneUserRequest request, CancellationToken ct = new CancellationToken())
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(request.EmailAddress))
            {
                var user = await _getOneUserQuery.ExecuteAsync(new EmailAddress(request.EmailAddress), ct);
                if (user == null) return NotFound();

                var userDto = new UserDto
                {
                    Guid = user.Guid,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    EmailAddress = user.EmailAddress
                };

                return Ok(new Response<UserDto>(userDto));
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return InternalServerError();
        }
    }
}