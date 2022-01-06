using Shopperior.Application.DependencyInjection.Microsoft;
using Shopperior.Data.EFCore.DependencyInjection.Microsoft;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddShopperiorEFCoreDbContext(new ShopperiorEFCoreConfiguration
{
    ConnectionString = config.GetConnectionString("ShopperiorDb"),
    DatabaseType = DatabaseType.SqlServer
});
builder.Services.AddShopperiorApplication(new ShopperiorApplicationConfiguration());
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
