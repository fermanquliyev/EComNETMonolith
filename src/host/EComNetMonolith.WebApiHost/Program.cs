using EComNetMonolith.Cart;
using EComNetMonolith.Order;
using EComNetMonolith.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCartModule(builder.Configuration)
    .AddOrderModule(builder.Configuration)
    .AddProductsModule(builder.Configuration);

var app = builder.Build();

app.UseCartModule()
    .UseOrderModule()
    .UseProductsModule() ;

app.MapGet("/", () => "Hello World!");

app.Run();
