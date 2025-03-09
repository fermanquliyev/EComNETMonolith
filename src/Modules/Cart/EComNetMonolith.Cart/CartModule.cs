using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EComNetMonolith.Cart
{
    public static class CartModule
    {
        public static IServiceCollection AddCartModule(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<ICartRepository, CartRepository>();
            //services.AddScoped<ICartItemRepository, CartItemRepository();

            return services;
        }

        public static IApplicationBuilder UseCartModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
