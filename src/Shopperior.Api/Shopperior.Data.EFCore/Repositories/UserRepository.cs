using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Contracts.Users.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories
{
    public class UserRepository : Repository<ShopperiorDbContext, User>, IUserRepository
    {
        public UserRepository(ShopperiorDbContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(long id)
        {
            return await Table.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetAsync(Guid guid)
        {
            return await Table.FirstOrDefaultAsync(u => u.Guid == guid);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await Table.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAddressAsync(string emailAddress)
        {
            return await Table.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }
    }
}