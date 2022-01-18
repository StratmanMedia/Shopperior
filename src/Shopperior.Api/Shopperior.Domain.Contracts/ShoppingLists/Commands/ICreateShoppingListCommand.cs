using Shopperior.Domain.Contracts.Shared.Cqrs;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Commands;

public interface ICreateShoppingListCommand : IBaseCommand<ICreateShoppingListRequest, ShoppingList>
{
    
}