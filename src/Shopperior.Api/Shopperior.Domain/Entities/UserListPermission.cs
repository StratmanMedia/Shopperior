using System;

namespace Shopperior.Domain.Entities;

public class UserListPermission
{
    public long Id { get; set; }
    public Guid Guid { get; set; }
    public long UserId { get; set; }
    public long ShoppingListId { get; set; }
    public string Permission { get; set; }
    public DateTime CreatedTime { get; set; }
}