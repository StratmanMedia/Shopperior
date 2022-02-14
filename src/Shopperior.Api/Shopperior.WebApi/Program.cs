using Loggly;
using Loggly.Config;
using Newtonsoft.Json;
using Serilog;
using Shopperior.Application.DependencyInjection.Microsoft;
using Shopperior.Data.EFCore.DependencyInjection.Microsoft;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Shared.Middleware;
using Shopperior.WebApi.Shared.Services;
using Shopperior.WebApi.ShoppingLists.Interfaces;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using Shopperior.WebApi.Users.Interfaces;
using Shopperior.WebApi.Users.Services;
using StratmanMedia.Auth;

var _appName = "Shopperior API";
Log.Logger = CreateLogger();
Log.Information($"{_appName} is starting.");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((ctx, lc) => lc
        .Enrich.WithProperty("Application", _appName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(ctx.Configuration));

    // Add services to the container.
    ConfigureServices(builder.Services, builder.Configuration);

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    Configure(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, $"{_appName} terminated unexpectedly.");
}
finally
{
    Log.Information($"{_appName} shutdown complete.");
    Log.CloseAndFlush();
}

Serilog.ILogger CreateLogger()
{
    return new LoggerConfiguration()
        .Enrich.WithProperty("Application", _appName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateBootstrapLogger();
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
        options.AddPolicy(name: "AllowSpecificOrigins",
            builder =>
            {
                var origins = config["AllowedOrigins"].Split(",");
                builder.WithOrigins(origins);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
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

    app.UseHttpsRedirection();
    app.UseCors("AllowSpecificOrigins");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCurrentUser();
    app.MapControllers();
}
