using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Exceptions;
using Shopperior.Domain.ValueObjects;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Users.Endpoints;

public class GetOneUserEndpoint : BaseEndpoint<GetOneUserEndpoint>
{
    private readonly ILogger<GetOneUserEndpoint> _logger;
    private readonly IGetOneUserQuery _getOneUserQuery;

    public GetOneUserEndpoint(
        ILogger<GetOneUserEndpoint> logger,
        IGetOneUserQuery getOneUserQuery)
    :base(logger)
    {
        _logger = logger;
        _getOneUserQuery = getOneUserQuery;
    }

    [Authorize]
    [HttpGet("/api/v1/users")]
    public async Task<ActionResult<Response<UserDto>>> HandleAsync(GetOneUserRequest request, CancellationToken ct = new CancellationToken())
    {
        return await TryActionAsync<UserDto>(() => EndpointAction(request, ct));
    }

    private async Task<UserDto> EndpointAction(GetOneUserRequest request, CancellationToken ct = new CancellationToken())
    {
        await ValidateRequest(request);

        var user = await _getOneUserQuery.ExecuteAsync(new EmailAddress(request.EmailAddress), ct);
        if (user == null)
            throw new ResourceNotFoundException($"User with Email Address ({request.EmailAddress}) was not found.");

        var userDto = new UserDto
        {
            Guid = user.Guid,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.EmailAddress
        };

        return userDto;
    }

    private Task ValidateRequest(GetOneUserRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.EmailAddress))
            throw new BadHttpRequestException("Email Address was not found in request.");

        return Task.CompletedTask;
    }
}