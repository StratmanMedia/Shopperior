using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Users.Queries
{
    public class GetOneUserQuery : IGetOneUserQuery
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public GetOneUserQuery(
            ILogger<GetOneUserQuery> logger,
            IUserRepository userRepository)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
        }

        public async Task<User> ExecuteAsync(Guid guid, CancellationToken cancellationToken = new())
        {
            try
            {
                var user = await _userRepository.GetAsync(guid);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(GetOneUserQuery)} (guid).");
                throw;
            }
        }

        public async Task<User> ExecuteAsync(string username, CancellationToken cancellationToken = new())
        {
            try
            {
                var user = await _userRepository.GetByUsernameAsync(username);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(GetOneUserQuery)} (username).");
                throw;
            }
        }
    }
}