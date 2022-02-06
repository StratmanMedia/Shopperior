using Shopperior.Domain.Contracts.ListItems.Models;

namespace Shopperior.Domain.Contracts.ListItems.Queries;

public interface IGetManyItemsByListQuery
{
    Task<IEnumerable<IListItemModel>> ExecuteAsync(Guid shoppingListGuid, CancellationToken ct = new CancellationToken());
}