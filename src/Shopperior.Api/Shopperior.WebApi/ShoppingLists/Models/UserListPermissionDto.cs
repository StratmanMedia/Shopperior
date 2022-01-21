namespace Shopperior.WebApi.ShoppingLists.Models;

public class UserListPermissionDto
{
    public Guid UserGuid { get; set; }
    public Guid ShoppingListGuid { get; set; }
    public string Permission { get; set; }
}