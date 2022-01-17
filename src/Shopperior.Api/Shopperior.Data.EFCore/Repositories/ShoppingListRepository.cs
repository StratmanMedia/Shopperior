using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class ShoppingListRepository : Repository<ShopperiorDbContext, ShoppingList>, IShoppingListRepository
{
    private readonly IUserListRepository _userListRepository;

    public ShoppingListRepository(ShopperiorDbContext context, IUserListRepository userListRepository) : base(context)
    {
        _userListRepository = userListRepository;
    }

    public async Task<IEnumerable<ShoppingList>> GetManyByUserAsync(long userId, CancellationToken ct = new())
    {
        var userPermissions = await _userListRepository.GetManyByUserAsync(userId, ct);
        var lists = Table.Where(l => userPermissions.Select(p => p.ShoppingListId).Contains(l.Id));

        return lists.AsEnumerable();
    }
}