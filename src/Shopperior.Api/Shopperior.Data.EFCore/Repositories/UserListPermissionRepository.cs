using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListPermissionRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListPermissionRepository
{
    public UserListPermissionRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<UserListPermission>> GetManyByUserAsync(long userId, CancellationToken ct = new())
    {
        var userPermissions = Context.UserListPermission.Where(p => p.UserId == userId);

        return Task.FromResult(userPermissions.AsEnumerable());
    }

    public Task<IEnumerable<UserListPermission>> GetManyByListAsync(long shoppingListId, CancellationToken ct = new())
    {
        var listPermissions = Context.UserListPermission.Where(p => p.ShoppingListId == shoppingListId);

        return Task.FromResult(listPermissions.AsEnumerable());
    }

    public Task<UserListPermission> GetOneAsync(long userId, long shoppingListId, CancellationToken ct = new())
    {
        var permission = Table.FirstOrDefault(p => p.UserId == userId && p.ShoppingListId == shoppingListId);

        return Task.FromResult(permission);
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