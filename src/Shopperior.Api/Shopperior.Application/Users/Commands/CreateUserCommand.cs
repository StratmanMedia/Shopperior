using System;
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

        public async Task<Response> ExecuteAsync(ICreateUserRequest request)
        {
            try
            {
                var validation = await ValidateRequest(request);
                if (!string.IsNullOrWhiteSpace(validation)) return new Response(validation);

                var entity = new User
                {
                    Guid = Guid.NewGuid(),
                    Username = request.Username,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailAddress = request.EmailAddress,
                    IsActive = true,
                    CreatedTime = DateTime.UtcNow
                };
                await _userRepository.CreateAsync(entity);

                return new Response();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(CreateUserCommand)}");
                throw;
            }
        }

        private async Task<string> ValidateRequest(ICreateUserRequest request)
        {
            var existing = await _userRepository.GetByUsernameAsync(request.Username);
            if (existing != null) return $"{nameof(request.Username)} of {request.Username} already exists.";

            existing = await _userRepository.GetByEmailAddressAsync(request.EmailAddress);
            if (existing != null) return $"{nameof(request.EmailAddress)} of {request.EmailAddress} already exists.";

            return null;
        }
    }
}