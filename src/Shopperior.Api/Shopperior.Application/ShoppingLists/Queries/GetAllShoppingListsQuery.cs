using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Queries;

public class GetAllShoppingListsQuery : IGetAllShoppingListsQuery
{
    private readonly IShoppingListRepository _shoppingListRepository;

    public GetAllShoppingListsQuery(IShoppingListRepository shoppingListRepository)
    {
        _shoppingListRepository = shoppingListRepository;
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(string username, CancellationToken cancellationToken = new())
    {
        var lists = await _shoppingListRepository.GetManyByUserAsync(username, cancellationToken);
        return lists;
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(long request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(Guid request, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }
}