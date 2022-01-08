using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using Shopperior.Application.Users.Models;
using Shopperior.Domain.Contracts.Users;
using Shopperior.WebApi.Shared.Endpoints;
using Shopperior.WebApi.Users.Models;
using StratmanMedia.Exceptions.Extensions;
using StratmanMedia.ResponseObjects;

namespace Shopperior.WebApi.Users.Endpoints;

public class PostUserEndpoint : BaseEndpoint
{
    private readonly ICreateUserCommand _createUserCommand;
    private readonly IGetOneUserQuery _getOneUserQuery;
    private readonly ILogger<PostUserEndpoint> _logger;

    public PostUserEndpoint(
        ILogger<PostUserEndpoint> logger,
        ICreateUserCommand createUserCommand,
        IGetOneUserQuery getOneUserQuery)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
        _createUserCommand = Guard.Against.Null(createUserCommand, nameof(createUserCommand));
        _getOneUserQuery = Guard.Against.Null(getOneUserQuery, nameof(getOneUserQuery));
    }

    [HttpPost("api/v1/users")]
    public async Task<ActionResult<Response<UserDto>>> HandleAsync([FromBody] UserDto request, CancellationToken cancellationToken = new CancellationToken())
    {
        try
        {
            var createUserRequest = new CreateUserRequest
            {
                Username = request.Username,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress
            };
            var createUserResponse = await _createUserCommand.ExecuteAsync(createUserRequest, cancellationToken);
            if (createUserResponse.IsSuccess) return Ok(new Response<UserDto>());
            
            var user = await _getOneUserQuery.ExecuteAsync(request.Username);
            if (user == null) return BadRequest(new Response<UserDto>(createUserResponse.Messages));

            var dto = new UserDto
            {
                Guid = user.Guid,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress
            };
            return Ok(new Response<UserDto>(dto));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.JoinAllMessages());
            return StatusCode(500);
        }
    }

}