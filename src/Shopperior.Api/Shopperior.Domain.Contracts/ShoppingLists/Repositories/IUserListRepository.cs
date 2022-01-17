using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IUserListRepository : IRepository<UserListPermission>
{
    Task<IEnumerable<UserListPermission>> GetManyByUserAsync(long userId, CancellationToken ct = new());
}