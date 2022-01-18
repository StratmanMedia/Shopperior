using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IValidateShoppingListPermissionRequest
{
    User User { get; set; }
    ShoppingList ShoppingList { get; set; }
    string Permission { get; set; }
}