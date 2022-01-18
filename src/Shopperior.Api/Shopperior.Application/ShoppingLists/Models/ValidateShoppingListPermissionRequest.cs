using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Models;

public class ValidateShoppingListPermissionRequest : IValidateShoppingListPermissionRequest
{
    public User User { get; set; }
    public ShoppingList ShoppingList { get; set; }
    public string Permission { get; set; }
}