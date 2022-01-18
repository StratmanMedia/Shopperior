using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IUserListPermissionRepository : IRepository<UserListPermission>
{
    Task<IEnumerable<UserListPermission>> GetManyByUserAsync(long userId, CancellationToken ct = new());
    Task<IEnumerable<UserListPermission>> GetManyByListAsync(long shoppingListId, CancellationToken ct = new());
}