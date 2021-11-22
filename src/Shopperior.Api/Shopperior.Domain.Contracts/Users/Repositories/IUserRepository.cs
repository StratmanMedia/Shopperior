using System;
using System.Threading.Tasks;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(long id);
        Task<User> GetAsync(Guid guid);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAddressAsync(string emailAddress);
    }
}