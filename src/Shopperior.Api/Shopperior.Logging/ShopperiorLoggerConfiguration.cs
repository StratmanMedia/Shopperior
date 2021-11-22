namespace Shopperior.Logging
{
    public class ShopperiorLoggerConfiguration
    {
        public string ApplicationName { get; set; }
        public ShopperiorLogLevel MinimumLogLevel { get; set; }
        public ShopperiorLoggerSqlServerConfiguration SqlServerConfiguration { get; set; }
    }
}