using System.Collections.Generic;
using System.Threading.Tasks;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.Users
{
    public interface IGetAllUsersQuery
    {
        Task<IEnumerable<User>> ExecuteAsync();
    }
}