using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class UserListRepository : Repository<ShopperiorDbContext, UserListPermission>, IUserListRepository
{
    public UserListRepository(ShopperiorDbContext context) : base(context)
    {
    }
}