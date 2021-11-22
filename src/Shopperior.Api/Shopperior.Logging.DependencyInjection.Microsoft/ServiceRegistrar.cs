using Microsoft.Extensions.DependencyInjection;

namespace Shopperior.Logging.DependencyInjection.Microsoft
{
    public class ServiceRegistrar
    {
        internal static void AddRequiredServices(IServiceCollection services, ShopperiorLoggerConfiguration configuration)
        {
            services.AddScoped<IShopperiorLogger>(provider => new ShopperiorLogger(configuration));
        }
    }
}