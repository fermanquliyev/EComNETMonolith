using EComNetMonolith.Cart;
using EComNetMonolith.Order;
using EComNetMonolith.Inventory;
using Scalar.AspNetCore;
using EComNetMonolith.Shared.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCartModule(builder.Configuration)
    .AddOrderModule(builder.Configuration)
    .AddInventoryModule(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.MapGet("/", () => Results.Redirect("/scalar"));
}

app.UseCartModule()
    .UseOrderModule()
    .UseInventoryModule();

app.UseExceptionHandler(options=> { });

app.Run();
