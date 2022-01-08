using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.Users.Queries
{
    public class GetAllUsersQuery : IGetAllUsersQuery
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;

        public GetAllUsersQuery(
            IUserRepository userRepository,
            ILogger<GetAllUsersQuery> logger)
        {
            _logger = Guard.Against.Null(logger, nameof(logger));
            _userRepository = Guard.Against.Null(userRepository, nameof(userRepository));
        }

        public async Task<IEnumerable<User>> ExecuteAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception occurred in {nameof(GetAllUsersQuery)}");
                throw;
            }
        }
    }
}