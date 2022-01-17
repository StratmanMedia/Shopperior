using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface ICreateShoppingListRequest
{
    Guid UserGuid { get; set; }
    ShoppingList ShoppingList { get; set; }
}