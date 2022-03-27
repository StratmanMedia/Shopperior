namespace Shopperior.Domain.Entities;

public class ShoppingList
{
    public long Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? LastModifiedTime { get; set; }
    public DateTime? TrashedTime { get; set; }
    public ICollection<UserListPermission> Permissions { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<ListItem> Items { get; set; }
}