﻿using Shopperior.Domain.Entities;

namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IUserListPermissionModel
{
    User User { get; set; }
    IShoppingListModel ShoppingList { get; set; }
    string Permission { get; set; }
}