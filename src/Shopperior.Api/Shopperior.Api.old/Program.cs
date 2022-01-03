using Shopperior.Application.DependencyInjection.Microsoft;
using Shopperior.Data.EFCore.DependencyInjection.Microsoft;
using Shopperior.Logging;
using Shopperior.Logging.DependencyInjection.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddShopperiorEFCoreDbContext(new ShopperiorEFCoreConfiguration
{
    DatabaseType = DatabaseType.SqlServer,
    ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection")
});
builder.Services.AddShopperiorLogging(new ShopperiorLoggerConfiguration
{
    ApplicationName = "Shopperior API",
    MinimumLogLevel = ShopperiorLogLevel.Debug,
    SqlServerConfiguration = new ShopperiorLoggerSqlServerConfiguration()
    {
        ConnectionString = builder.Configuration.GetConnectionString("SqlServerConnection"),
        TableName = "Logs"
    }
});
builder.Services.AddShopperiorApplication(new ShopperiorApplicationConfiguration());

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowSpecificOrigins",
        corsBuilder =>
        {
            var origins = builder.Configuration["AllowedOrigins"].Split(",");
            corsBuilder.WithOrigins(origins);
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
