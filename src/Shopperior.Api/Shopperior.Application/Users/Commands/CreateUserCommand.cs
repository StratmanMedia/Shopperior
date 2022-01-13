using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Application.Users.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public CreateUserCommand(
            IUserRepository userRepository,
            ILogger<CreateUserCommand> logger)
        {
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
            _logger = Guard.Against.Null(logger, nameof(logger));
        }

        public async Task<Response<User>> ExecuteAsync(User request, CancellationToken cancellationToken = new())
        {
            try
            {
                var validation = await ValidateRequest(request);
                if (!validation.IsSuccess) return new Response<User>(validation.Messages);

                var entity = new User
                {
                    Guid = Guid.NewGuid(),
                    Username = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailAddress = request.EmailAddress,
                    Idp = request.Idp,
                    IdpSubject = request.IdpSubject,
                    CreatedTime = DateTime.UtcNow
                };
                await _userRepository.CreateAsync(entity);

                return new Response<User>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(CreateUserCommand)}");
                throw;
            }
        }

        private async Task<Response> ValidateRequest(User request)
        {
            var existing = await _userRepository.GetByEmailAddressAsync(request.EmailAddress);
            if (existing != null)
                return new Response($"{nameof(request.EmailAddress)} of {request.EmailAddress} already exists.");

            return new Response();
        }
    }
}