namespace Shopperior.WebApi.ShoppingLists.Models;

public class ShoppingListDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public IEnumerable<UserListPermissionDto> Permissions { get; set; }
    public IEnumerable<ListItemDto> Items { get; set; }
}