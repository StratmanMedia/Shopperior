using System;
using Shopperior.Domain.Contracts.ShoppingLists.Models;
using Shopperior.Domain.Entities;

namespace Shopperior.Application.ShoppingLists.Models;

public class CreateShoppingListRequest : ICreateShoppingListRequest
{
    public Guid UserGuid { get; set; }
    public ShoppingList ShoppingList { get; set; }
}