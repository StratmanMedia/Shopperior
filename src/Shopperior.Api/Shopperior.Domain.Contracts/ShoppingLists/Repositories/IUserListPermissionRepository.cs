using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Domain.Contracts.ShoppingLists.Repositories;

public interface IUserListPermissionRepository : IRepository<UserListPermission>
{
    Task<UserListPermission[]> GetManyByUserAsync(long userId, CancellationToken ct = new());
    Task<UserListPermission[]> GetManyByListAsync(long shoppingListId, CancellationToken ct = new());
    Task<UserListPermission> GetOneAsync(long userId, long shoppingListId, CancellationToken ct = new());
    Task<UserListPermission> GetOneAsync(Guid userGuid, Guid shoppingListGuid, CancellationToken ct = new());
}