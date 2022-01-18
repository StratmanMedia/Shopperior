using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Models;

public class DeleteShoppingListRequest : IDeleteShoppingListRequest
{
    public Guid UserGuid { get; set; }
    public Guid ShoppingListGuid { get; set; }
}