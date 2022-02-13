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

try
{
    var builder = WebApplication.CreateBuilder(args);

    Log.Logger = CreateLogger(builder.Configuration);
    Log.Information($"{_appName} is starting.");

    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(builder.Configuration));

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
    Log.CloseAndFlush();
}

Serilog.ILogger CreateLogger(IConfiguration config)
{
    ConfigureLoggly(config);
    return new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .CreateBootstrapLogger();
}

void ConfigureLoggly(IConfiguration config)
{
    var logglyConfig = LogglyConfig.Instance;
    logglyConfig.CustomerToken = config["Serilog:Loggly:CustomerToken"];
    logglyConfig.ApplicationName = config["Serilog:Loggly:ApplicationName"];
    logglyConfig.Transport = new TransportConfiguration()
    {
        EndpointHostname = config["Serilog:Loggly:EndpointHostname"],
        EndpointPort = int.Parse(config["Serilog:Loggly:EndpointPort"]),
        LogTransport = LogTransport.Https
    };
    logglyConfig.IsEnabled = bool.Parse(config["Serilog:Loggly:IsEnabled"]);
    logglyConfig.ThrowExceptions = bool.Parse(config["Serilog:Loggly:ThrowExceptions"]);
    logglyConfig.TagConfig.Tags.AddRange(new ITag[]{
        new ApplicationNameTag {Formatter = "Application-{0}"},
        new HostnameTag { Formatter = "Host-{0}" }
    });
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
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCurrentUser();
    app.MapControllers();
}
