﻿using Microsoft.Extensions.DependencyInjection;
using Shopperior.Application.Users.Commands;
using Shopperior.Application.Users.Queries;
using Shopperior.Domain.Contracts.Users;

namespace Shopperior.Application.DependencyInjection.Microsoft;

internal static class ServiceRegistrar
{
    internal static void AddRequiredServices(IServiceCollection services, ShopperiorApplicationConfiguration configuration)
    {
        services.AddScoped<ICreateUserCommand, CreateUserCommand>();
        services.AddScoped<IGetOneUserQuery, GetOneUserQuery>();
    }
}