using Ardalis.SmartEnum;

namespace Shopperior.Domain.Enumerations
{
    public class ShoppingListPermission : SmartEnum<ShoppingListPermission>
    {
        public static readonly ShoppingListPermission Viewer = new("VIEWER", 0);
        public static readonly ShoppingListPermission Contributor = new("CONTRIBUTOR", 1);
        public static readonly ShoppingListPermission Administrator = new("ADMINISTRATOR", 2);
        public static readonly ShoppingListPermission Owner = new("OWNER", 3);

        private ShoppingListPermission(string name, int value) : base(name, value)
        {

        }

        
    }
}