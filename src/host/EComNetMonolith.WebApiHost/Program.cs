using EComNetMonolith.Cart;
using EComNetMonolith.Order;
using EComNetMonolith.Inventory;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCartModule(builder.Configuration)
    .AddOrderModule(builder.Configuration)
    .AddInventoryModule(builder.Configuration);

var app = builder.Build();

app.UseCartModule()
    .UseOrderModule()
    .UseInventoryModule() ;

app.MapGet("/", () => "Hello World!");

app.Run();
