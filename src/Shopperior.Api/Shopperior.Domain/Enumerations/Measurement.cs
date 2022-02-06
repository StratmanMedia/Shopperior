using Ardalis.SmartEnum;

namespace Shopperior.Domain.Enumerations
{
    public class Measurement : SmartEnum<Measurement>
    {
        public static readonly Measurement Each = new Measurement("ea", 0);
        public static readonly Measurement Pound = new Measurement("lb", 1);
        public static readonly Measurement Ounce = new Measurement("oz", 2);
        public static readonly Measurement Gallon = new Measurement("gal", 3);
        public static readonly Measurement Quart = new Measurement("qt", 4);
        public static readonly Measurement Pint = new Measurement("pt", 5);
        public static readonly Measurement Kilogram = new Measurement("kg", 6);
        public static readonly Measurement Gram = new Measurement("g", 7);
        public static readonly Measurement Liter = new Measurement("L", 8);
        public static readonly Measurement Milliliter = new Measurement("mL", 9);
        public static readonly Measurement Bag = new Measurement("bag", 10);
        public static readonly Measurement Bottle = new Measurement("bottle", 11);
        public static readonly Measurement Box = new Measurement("box", 12);
        public static readonly Measurement Case = new Measurement("case", 13);
        public static readonly Measurement Pack = new Measurement("pack", 14);
        public static readonly Measurement Roll = new Measurement("roll", 15);

        private Measurement(string name, int value) : base(name, value)
        {
        }
    }
}