using Microsoft.Extensions.Logging;
using Shopperior.Domain.Contracts.ListItems.Models;
using Shopperior.Domain.Contracts.ListItems.Queries;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;

namespace Shopperior.Application.ListItems.Queries;

public class GetManyItemsByListQuery : IGetManyItemsByListQuery
{
    private readonly ILogger<GetManyItemsByListQuery> _logger;
    private readonly IShoppingListRepository _shoppingListRepository;
    private readonly IListItemRepository _listItemRepository;
    private readonly IListItemModelResolver _listItemModelResolver;

    public GetManyItemsByListQuery(
        ILogger<GetManyItemsByListQuery> logger,
        IShoppingListRepository shoppingListRepository,
        IListItemRepository listItemRepository,
        IListItemModelResolver listItemModelResolver)
    {
        _logger = logger;
        _shoppingListRepository = shoppingListRepository;
        _listItemRepository = listItemRepository;
        _listItemModelResolver = listItemModelResolver;
    }

    public async Task<IEnumerable<IListItemModel>> ExecuteAsync(Guid shoppingListGuid, CancellationToken ct = new CancellationToken())
    {
        var list = await _shoppingListRepository.GetOneAsync(shoppingListGuid, ct);
        if (list == null)
            return Array.Empty<IListItemModel>();

        var items = await _listItemRepository.GetManyByListAsync(list.Id);
        var itemModels = new List<IListItemModel>();
        foreach (var i in items)
        {
            var model = await _listItemModelResolver.ResolveAsync(i);
            itemModels.Add(model);
        }

        return itemModels;
    }
}