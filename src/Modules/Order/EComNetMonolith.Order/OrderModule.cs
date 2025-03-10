using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EComNetMonolith.Order
{
    public static class OrderModule
    {
        public static IServiceCollection AddOrderModule(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }

        public static IApplicationBuilder UseOrderModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
