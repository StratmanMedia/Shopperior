using Microsoft.EntityFrameworkCore;
using Serilog;
using Shopperior.Application.DependencyInjection.Microsoft;
using Shopperior.Data.EFCore;
using Shopperior.Data.EFCore.DependencyInjection.Microsoft;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Shared.Middleware;
using Shopperior.WebApi.Shared.Services;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using Shopperior.WebApi.Users.Interfaces;
using Shopperior.WebApi.Users.Services;
using StratmanMedia.Auth;

const string appName = "Shopperior API";
ConfigureLogging();

try
{
    var builder = WebApplication.CreateBuilder(args);
    var config = builder.Configuration;

    // Add services to the container.
    ConfigureServices(builder.Services, config);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    Configure(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"{appName} terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}


void ConfigureLogging()
{
    //var config = new ConfigurationBuilder()
    //    .SetBasePath(Directory.GetCurrentDirectory())
    //    .AddJsonFile(@"appsettings.json")
    //    .AddJsonFile(@"appsettings.Development.json")
    //    .Build()
    //    .GetSection("Logging");
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", appName)
        .CreateLogger();
}

void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    services.AddShopperiorEFCoreDbContext(new ShopperiorEFCoreConfiguration
    {
        ConnectionString = config.GetConnectionString("ShopperiorDb"),
        DatabaseType = DatabaseType.SqlServer
    });
    services.AddShopperiorApplication(new ShopperiorApplicationConfiguration());
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policyBuilder =>
            {
                // TODO: use a white list to be more secure
                policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
    });
    services.AddStratmanMediaAuthentication(options =>
    {
        options.Authority = config.GetValue<string>("OIDC:Authority") + "/";
        options.Audience = config.GetValue<string>("OIDC:Audience");
        options.Scopes = config.GetSection("OIDC:Scopes").GetChildren().Select(c => c.Value).ToArray();
    });
    services.AddScoped<ICurrentUserResolver, CurrentUserResolver>();
    services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
    services.AddScoped<IUserDtoResolver, UserDtoResolver>();
    services.AddScoped<IListPermissionDtoResolver, ListPermissionDtoResolver>();
    services.AddScoped<IListItemDtoResolver, ListItemDtoResolver>();
    services.AddScoped<IShoppingListDtoResolver, ShoppingListDtoResolver>();
    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ShopperiorDbContext>();
        context.Database.Migrate();
    }

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCurrentUser();
    app.MapControllers();
}
