using Microsoft.EntityFrameworkCore;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListPermissionRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListPermissionRepository
{
    public UserListPermissionRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public async Task<UserListPermission[]> GetManyByUserAsync(long userId, CancellationToken ct = default)
    {
        var userPermissions = Context.UserListPermission.Where(p => p.UserId == userId);

        return await Task.FromResult(userPermissions.ToArray());
    }

    public async Task<UserListPermission[]> GetManyByListAsync(long shoppingListId, CancellationToken ct = default)
    {
        var listPermissions = Context.UserListPermission.Where(p => p.ShoppingListId == shoppingListId);

        return await Task.FromResult(listPermissions.ToArray());
    }

    public async Task<UserListPermission> GetOneAsync(long userId, long shoppingListId, CancellationToken ct = default)
    {
        var permission = Table.FirstOrDefault(p => p.UserId == userId && p.ShoppingListId == shoppingListId);

        return await Task.FromResult(permission);
    }

    public async Task<UserListPermission> GetOneAsync(Guid userGuid, Guid shoppingListGuid, CancellationToken ct = default)
    {
        var query = Table
            .Include(p => p.User)
            .Include(p => p.ShoppingList);
        var permission = query.FirstOrDefault(p => p.User.Guid == userGuid && p.ShoppingList.Guid == shoppingListGuid);

        return await Task.FromResult(permission);
    }
}