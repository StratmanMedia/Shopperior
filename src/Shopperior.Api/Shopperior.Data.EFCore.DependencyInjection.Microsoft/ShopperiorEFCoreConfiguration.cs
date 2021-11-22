namespace Shopperior.Data.EFCore.DependencyInjection.Microsoft;

public class ShopperiorEFCoreConfiguration
{
    public DatabaseType DatabaseType { get; set; }
    public string ConnectionString { get; set; }
}