using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListRepository
{
    public UserListRepository(ShopperiorDbContext context) : base(context)
    {
    }

    public Task<IEnumerable<UserListPermission>> GetManyByUserAsync(long userId, CancellationToken ct = new CancellationToken())
    {
        var userPermissions = Context.UserListPermission.Where(p => p.UserId == userId);

        return Task.FromResult(userPermissions.AsEnumerable());
    }
}