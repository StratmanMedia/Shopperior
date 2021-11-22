using Microsoft.Extensions.DependencyInjection;

namespace Shopperior.Data.EFCore.DependencyInjection.Microsoft;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShopperiorApplication(this IServiceCollection services, ShopperiorEFCoreConfiguration configuration)
    {
        ServiceRegistrar.AddRequiredServices(services, configuration);

        return services;
    }
}