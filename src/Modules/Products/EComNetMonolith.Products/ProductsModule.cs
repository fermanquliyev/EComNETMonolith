using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EComNetMonolith.Products
{
    public static class ProductsModule
    {
        public static IServiceCollection AddProductsModule(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<ICartService, CartService>();
            //services.AddScoped<ICartRepository, CartRepository>();
            //services.AddScoped<ICartItemRepository, CartItemRepository();

            return services;
        }

        public static IApplicationBuilder UseProductsModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
