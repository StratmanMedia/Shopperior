using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;
using StratmanMedia.ResponseObjects;

namespace Shopperior.Domain.Contracts.ShoppingLists.Queries;

public interface IValidateShoppingListPermissionQuery : IBaseQuery<IValidateShoppingListPermissionRequest, Response>
{
    Task<Response> ExecuteAsync(User user, Guid shoppingListGuid, string permission, CancellationToken ct = new());
    Task<Response> ExecuteAsync(Guid userGuid, ShoppingList shoppingList, string permission, CancellationToken ct = new());
    Task<Response> ExecuteAsync(Guid userGuid, Guid shoppingListGuid, string permission, CancellationToken ct = new());
}