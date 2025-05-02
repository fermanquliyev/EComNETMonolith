using EComNetMonolith.Cart;
using EComNetMonolith.Order;
using EComNetMonolith.Inventory;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCartModule(builder.Configuration)
    .AddOrderModule(builder.Configuration)
    .AddInventoryModule(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar"));
}

app.UseCartModule()
    .UseOrderModule()
    .UseInventoryModule() ;

app.Run();
