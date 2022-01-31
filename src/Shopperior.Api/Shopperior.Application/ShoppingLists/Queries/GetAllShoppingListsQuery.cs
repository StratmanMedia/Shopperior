using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;
using Shopperior.Domain.Contracts.Users;

namespace Shopperior.Application.ShoppingLists.Queries;

public class GetAllShoppingListsQuery : IGetAllShoppingListsQuery
{
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IGetOneUserQuery _getOneUserQuery;
    private readonly IShoppingListModelResolver _shoppingListModelResolver;

    public GetAllShoppingListsQuery(
        IShoppingListRepository shoppingListRepository,
        IGetOneUserQuery getOneUserQuery,
        IShoppingListModelResolver shoppingListModelResolver)
    {
        _shoppingListRepository = shoppingListRepository;
        _getOneUserQuery = getOneUserQuery;
        _shoppingListModelResolver = shoppingListModelResolver;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(string username, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(username, ct);
        var lists = await ExecuteAsync(user.Id, ct);

        return lists;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(long userId, CancellationToken ct = new())
    {
        var entities = await _shoppingListRepository.GetManyByUserAsync(userId, ct);
        var lists = new List<IShoppingListModel>();
        foreach (var entity in entities.ToArray())
        {
            var list = await _shoppingListModelResolver.ResolveAsync(entity);
            lists.Add(list);
        }

        return lists;
    }

    public async Task<IEnumerable<IShoppingListModel>> ExecuteAsync(Guid userGuid, CancellationToken ct = new())
    {
        var user = await _getOneUserQuery.ExecuteAsync(userGuid, ct);
        var lists = await ExecuteAsync(user.Id, ct);

        return lists;
    }
}