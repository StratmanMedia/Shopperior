using Shopperior.Application.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Queries;

public class GetOneShoppingListQuery : IGetOneShoppingListQuery
{
    private readonly IShoppingListRepository _shoppingListRepository;

    public GetOneShoppingListQuery(
        IShoppingListRepository shoppingListRepository)
    {
        _shoppingListRepository = shoppingListRepository;
    }

    public Task<IShoppingListModel> ExecuteAsync(long request, CancellationToken ct = new())
    {
        throw new NotImplementedException();
    }

    public async Task<IShoppingListModel> ExecuteAsync(Guid guid, CancellationToken ct = new())
    {
        var shoppingList = await _shoppingListRepository.GetOneAsync(guid, ct);
        var model = new ShoppingListModel
        {
            Guid = shoppingList.Guid,
            Name = shoppingList.Name
        };

        return model;
    }
}