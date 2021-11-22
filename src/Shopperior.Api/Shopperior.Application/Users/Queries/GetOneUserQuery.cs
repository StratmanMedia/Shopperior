using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;
using Shopperior.Logging;

namespace Shopperior.Application.Users.Queries
{
    public class GetOneUserQuery : IGetOneUserQuery
    {
        private readonly IShopperiorLogger _logger;
        private readonly IUserRepository _userRepository;

        public GetOneUserQuery(
            IShopperiorLogger logger,
            IUserRepository userRepository)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
        }

        public async Task<User> ExecuteAsync(Guid guid)
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

        public async Task<User> ExecuteAsync(string username)
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