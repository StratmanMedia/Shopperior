using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Users;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Queries;

public class GetAllShoppingListsQuery : IGetAllShoppingListsQuery
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IGetOneUserQuery _getOneUserQuery;

    public GetAllShoppingListsQuery(
        IShoppingListRepository shoppingListRepository,
        IGetOneUserQuery getOneUserQuery)
    {
        _shoppingListRepository = shoppingListRepository;
        _getOneUserQuery = getOneUserQuery;
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(string username, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(username, ct);
        var lists = await ExecuteAsync(user.Id, ct);

        return lists;
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(long request, CancellationToken ct = new())
    {
        var lists = await _shoppingListRepository.GetManyByUserAsync(request, ct);

        return lists;
    }

    public async Task<IEnumerable<ShoppingList>> ExecuteAsync(Guid request, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(request, ct);
        var lists = await ExecuteAsync(user.Id, ct);

        return lists;
    }
}