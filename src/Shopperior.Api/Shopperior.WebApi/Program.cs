using Shopperior.Application.DependencyInjection.Microsoft;
using Shopperior.Data.EFCore.DependencyInjection.Microsoft;
using Shopperior.WebApi.Shared.Interfaces;
using Shopperior.WebApi.Shared.Middleware;
using Shopperior.WebApi.Shared.Services;
using Shopperior.WebApi.ShoppingLists.Resolvers;
using StratmanMedia.Auth;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
{
    builder.Services.AddShopperiorEFCoreDbContext(new ShopperiorEFCoreConfiguration
    {
        ConnectionString = config.GetConnectionString("ShopperiorDb"),
        DatabaseType = DatabaseType.SqlServer
    });
    builder.Services.AddShopperiorApplication(new ShopperiorApplicationConfiguration());
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            policyBuilder =>
            {
                // TODO: use a white list to be more secure
                policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
    });
    builder.Services.AddStratmanMediaAuthentication(options =>
    {
        options.Authority = config.GetValue<string>("OIDC:Authority")+"/";
        options.Audience = config.GetValue<string>("OIDC:Audience");
        options.Scopes = config.GetSection("OIDC:Scopes").GetChildren().Select(c => c.Value).ToArray();
    });
    builder.Services.AddScoped<ICurrentUserResolver, CurrentUserResolver>();
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
    builder.Services.AddScoped<IListItemDtoResolver, ListItemDtoResolver>();
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //using (var scope = app.Services.CreateScope())
    //{
    //    var services = scope.ServiceProvider;
    //    var context = services.GetRequiredService<ShopperiorDbContext>();
    //    context.Database.Migrate();
    //}

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseCurrentUser();
    app.MapControllers();
}

app.Run();
