using Ardalis.SmartEnum;

namespace Shopperior.Domain.Enumerations
{
    public class ShoppingListPermission : SmartEnum<ShoppingListPermission>
    {
        public readonly ShoppingListPermission Owner = new("Owner", 0);
        public readonly ShoppingListPermission Administrator = new("Administrator", 1);
        public readonly ShoppingListPermission Contributor = new("Contributor", 2);
        public readonly ShoppingListPermission Viewer = new("Viewer", 3);

        private ShoppingListPermission(string name, int value) : base(name, value)
        {

        }
    }
}