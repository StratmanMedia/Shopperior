using Microsoft.Extensions.DependencyInjection;

namespace Shopperior.Application.DependencyInjection.Microsoft;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddShopperiorApplication(this IServiceCollection services, ShopperiorApplicationConfiguration configuration)
    {
        ServiceRegistrar.AddRequiredServices(services, configuration);

        return services;
    }
}