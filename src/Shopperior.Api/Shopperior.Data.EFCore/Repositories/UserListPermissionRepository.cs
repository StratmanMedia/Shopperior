using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListPermissionRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListPermissionRepository
{
    public UserListPermissionRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<UserListPermission>> GetManyByUserAsync(long userId, CancellationToken ct = new CancellationToken())
    {
        var userPermissions = Context.UserListPermission.Where(p => p.UserId == userId);

        return Task.FromResult(userPermissions.AsEnumerable());
    }

    public Task<IEnumerable<UserListPermission>> GetManyByListAsync(long shoppingListId, CancellationToken ct = new CancellationToken())
    {
        var listPermissions = Context.UserListPermission.Where(p => p.ShoppingListId == shoppingListId);

        return Task.FromResult(listPermissions.AsEnumerable());
    }
}