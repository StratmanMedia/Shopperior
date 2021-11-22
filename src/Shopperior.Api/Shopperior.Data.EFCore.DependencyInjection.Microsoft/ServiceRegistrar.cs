using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopperior.Data.EFCore.Repositories;
using Shopperior.Domain.Contracts.Categories.Repositories;
using Shopperior.Domain.Contracts.Items.Repositories;
using Shopperior.Domain.Contracts.ListItems.Repositories;
using Shopperior.Domain.Contracts.ShoppingLists.Repositories;
using Shopperior.Domain.Contracts.Stores.Repositories;
using Shopperior.Domain.Contracts.Users.Repositories;

namespace Shopperior.Data.EFCore.DependencyInjection.Microsoft;

internal static class ServiceRegistrar
{
    internal static void AddRequiredServices(IServiceCollection services, ShopperiorEFCoreConfiguration configuration)
    {
        services.AddDbContext<ShopperiorDbContext>(builder => builder.UseSqlServer(configuration.ConnectionString));
        services.AddScoped<IShopperiorDbContext>(provider => provider.GetService<ShopperiorDbContext>());

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IListItemRepository, ListItemRepository>();
        services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}