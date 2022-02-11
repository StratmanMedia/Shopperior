using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.WebApi.ShoppingLists.Models;

namespace Shopperior.WebApi.ShoppingLists.Interfaces;

public interface IShoppingListDtoResolver
{
    Task<ShoppingListDto> ResolveAsync(IShoppingListModel model, CancellationToken ct = default);
}