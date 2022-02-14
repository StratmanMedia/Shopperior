using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListPermissionRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListPermissionRepository
{
    public UserListPermissionRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public async Task<UserListPermission[]> GetManyByUserAsync(long userId, CancellationToken ct = new())
    {
        var userPermissions = Context.UserListPermission.Where(p => p.UserId == userId);

        return await Task.FromResult(userPermissions.ToArray());
    }

    public async Task<UserListPermission[]> GetManyByListAsync(long shoppingListId, CancellationToken ct = new())
    {
        var listPermissions = Context.UserListPermission.Where(p => p.ShoppingListId == shoppingListId);

        return await Task.FromResult(listPermissions.ToArray());
    }

    public async Task<UserListPermission> GetOneAsync(long userId, long shoppingListId, CancellationToken ct = new())
    {
        var permission = Table.FirstOrDefault(p => p.UserId == userId && p.ShoppingListId == shoppingListId);

        return await Task.FromResult(permission);
    }

    public async Task<UserListPermission> GetOneAsync(Guid userGuid, Guid shoppingListGuid, CancellationToken ct = new())
    {
        var user = Context.User.FirstOrDefault(u => u.Guid == userGuid);
        if (user == null) return null;

        var shoppingList = Context.ShoppingList.FirstOrDefault(l => l.Guid == shoppingListGuid);
        if (shoppingList == null) return null;

        return await GetOneAsync(user.Id, shoppingList.Id, ct);
    }
}