using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Resolvers;

public interface IShoppingListModelResolver
{
    Task<IShoppingListModel> ResolveAsync(ShoppingList entity);
    Task<ShoppingList> ResolveAsync(IShoppingListModel model);
}