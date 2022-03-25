using Microsoft.Extensions.DependencyInjection;
using Shopperior.Application.Categories.Commands;
using Shopperior.Application.Categories.Queries;
using Shopperior.Application.Categories.Services;
using Shopperior.Application.ListItems.Commands;
using Shopperior.Application.Shared.Queries;
using Shopperior.Application.ShoppingLists.Commands;
using Shopperior.Application.ShoppingLists.Queries;
using Shopperior.Application.ShoppingLists.Resolvers;
using Shopperior.Application.Users.Commands;
using Shopperior.Application.Users.Queries;
using Shopperior.Domain.Contracts.Categories.Commands;
using Shopperior.Domain.Contracts.Categories.Queries;
using Shopperior.Domain.Contracts.Categories.Services;
using Shopperior.Domain.Contracts.ListItems.Commands;
using Shopperior.Domain.Contracts.Shared.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Commands;
using Shopperior.Domain.Contracts.ShoppingLists.Queries;
using Shopperior.Domain.Contracts.ShoppingLists.Resolvers;
using Shopperior.Domain.Contracts.Users;

namespace Shopperior.Application.DependencyInjection.Microsoft;

internal static class ServiceRegistrar
{
    internal static void AddRequiredServices(IServiceCollection services, ShopperiorApplicationConfiguration configuration)
    {
        services.AddScoped<IDatabaseStatusQuery, DatabaseStatusQuery>();
        services.AddScoped<ICreateUserCommand, CreateUserCommand>();
        services.AddScoped<ICreateCategoryCommand, CreateCategoryCommand>();
        services.AddScoped<IGetOneCategoryQuery, GetOneCategoryQuery>();
        services.AddScoped<IGetOneUserQuery, GetOneUserQuery>();
        services.AddScoped<IGetAllCategoriesByShoppingListQuery, GetAllCategoriesByShoppingListQuery>();
        services.AddScoped<IGetAllShoppingListsQuery, GetAllShoppingListsQuery>();
        services.AddScoped<IGetOneShoppingListQuery, GetOneShoppingListQuery>();
        services.AddScoped<ICreateShoppingListCommand, CreateShoppingListCommand>();
        services.AddScoped<IDeleteShoppingListCommand, DeleteShoppingListCommand>();
        services.AddScoped<IUpdateShoppingListCommand, UpdateShoppingListCommand>();
        services.AddScoped<IValidateShoppingListPermissionQuery, ValidateShoppingListPermissionQuery>();
        services.AddScoped<ICreateListItemCommand, CreateListItemCommand>();
        services.AddScoped<IUpdateListItemCommand, UpdateListItemCommand>();
        services.AddScoped<IShoppingListModelResolver, ShoppingListModelResolver>();
        services.AddScoped<IListPermissionModelResolver, ListPermissionModelResolver>();
        services.AddScoped<IListItemModelResolver, ListItemModelResolver>();
        services.AddScoped<ICategoryModelResolver, CategoryModelResolver>();

    }
}