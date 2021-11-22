using Microsoft.Extensions.DependencyInjection;

namespace Shopperior.Logging.DependencyInjection.Microsoft
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddShopperiorLogging(this IServiceCollection services, ShopperiorLoggerConfiguration configuration)
        {
            ServiceRegistrar.AddRequiredServices(services, configuration);

            return services;
        }
    }
}