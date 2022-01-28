using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.ShoppingLists.Queries;

public interface IGetOneShoppingListQuery
{
    Task<IShoppingListModel> ExecuteAsync(long id, CancellationToken ct = new CancellationToken());
    Task<IShoppingListModel> ExecuteAsync(Guid guid, CancellationToken ct = new CancellationToken());
}