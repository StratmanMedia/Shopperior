using Ardalis.SmartEnum;

namespace Shopperior.Domain.Enumerations
{
    public class ShoppingListPermission : SmartEnum<ShoppingListPermission>
    {
        public static readonly ShoppingListPermission Viewer = new("Viewer", 0);
        public static readonly ShoppingListPermission Contributor = new("Contributor", 1);
        public static readonly ShoppingListPermission Administrator = new("Administrator", 2);
        public static readonly ShoppingListPermission Owner = new("Owner", 3);

        private ShoppingListPermission(string name, int value) : base(name, value)
        {

        }
    }
}