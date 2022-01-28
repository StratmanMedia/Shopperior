using Microsoft.Extensions.DependencyInjection;
using Shopperior.Application.ListItems.Commands;
using Shopperior.Application.ListItems.Resolvers;
using Shopperior.Application.ShoppingLists.Commands;
using Shopperior.Application.ShoppingLists.Queries;
using Shopperior.Application.Users.Commands;
using Shopperior.Application.Users.Queries;
using Shopperior.Domain.Contracts.ListItems.Commands;
using Shopperior.Domain.Contracts.ListItems.Resolvers;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.Users;

namespace Shopperior.Application.DependencyInjection.Microsoft;

internal static class ServiceRegistrar
{
    internal static void AddRequiredServices(IServiceCollection services, ShopperiorApplicationConfiguration configuration)
    {
        services.AddScoped<ICreateUserCommand, CreateUserCommand>();
        services.AddScoped<IGetOneUserQuery, GetOneUserQuery>();
        services.AddScoped<IGetAllShoppingListsQuery, GetAllShoppingListsQuery>();
        services.AddScoped<IGetOneShoppingListQuery, GetOneShoppingListQuery>();
        services.AddScoped<ICreateShoppingListCommand, CreateShoppingListCommand>();
        services.AddScoped<IDeleteShoppingListCommand, DeleteShoppingListCommand>();
        services.AddScoped<IUpdateShoppingListCommand, UpdateShoppingListCommand>();
        services.AddScoped<IValidateShoppingListPermissionQuery, ValidateShoppingListPermissionQuery>();
        services.AddScoped<ICreateListItemCommand, CreateListItemCommand>();
        services.AddScoped<IListItemModelResolver, ListItemModelResolver>();
    }
}