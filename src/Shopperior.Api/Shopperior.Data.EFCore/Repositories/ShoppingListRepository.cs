﻿using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;
using StratmanMedia.Repositories.EFCore;

namespace Shopperior.Data.EFCore.Repositories;

public class ShoppingListRepository : Repository<ShopperiorDbContext, ShoppingList>, IShoppingListRepository
{
    private readonly IUserListPermissionRepository _userListRepository;

    public ShoppingListRepository(
        ShopperiorDbContext context,
        IUserListPermissionRepository userListRepository) : base(context)
    {
        _userListRepository = userListRepository;
    }

    public async Task<ShoppingList> GetOneAsync(long id, CancellationToken ct = new CancellationToken())
    {
        var shoppingList = Table.FirstOrDefault(l => l.Id == id);

        return await Task.FromResult(shoppingList);
    }

    public async Task<ShoppingList> GetOneAsync(Guid guid, CancellationToken ct = new())
    {
        var shoppingList = Table.FirstOrDefault(l => l.Guid == guid);

        return await Task.FromResult(shoppingList);
    }

    public async Task<IEnumerable<ShoppingList>> GetManyByUserAsync(long userId, CancellationToken ct = new())
    {
        var userPermissions = await _userListRepository.GetManyByUserAsync(userId, ct);
        var lists = Table.Where(l => userPermissions.Select(p => p.ShoppingListId).Contains(l.Id));

        return lists.AsEnumerable();
    }
}