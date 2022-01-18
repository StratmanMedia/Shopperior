using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IDeleteShoppingListRequest
{
    Guid UserGuid { get; set; }
    Guid ShoppingListGuid { get; set; }
}