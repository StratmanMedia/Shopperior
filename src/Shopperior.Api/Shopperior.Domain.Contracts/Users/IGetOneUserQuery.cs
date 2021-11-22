using System;
using System.Threading.Tasks;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Users
{
    public interface IGetOneUserQuery
    {
        Task<User> ExecuteAsync(Guid guid);
        Task<User> ExecuteAsync(string username);
    }
}