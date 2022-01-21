namespace Shopperior.Domain.Contracts.ShoppingLists.Models;

public interface IUserListPermissionModel
{
    Guid UserGuid { get; set; }
    Guid ShoppingListGuid { get; set; }
    string Permission { get; set; }
}