using Shopperior.Domain.Enumerations;

namespace Shopperior.Domain.Entities
{
    public class UserListPermission
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public long ShoppingListId { get; set; }
        public ShoppingList ShoppingList { get; set; }
        public string Permission { get; set; }
    }
}