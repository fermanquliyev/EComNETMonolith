using EComNetMonolith.Cart.Data;
using EComNetMonolith.Shared.Data;
using EComNetMonolith.Shared.Data.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EComNetMonolith.Cart
{
    public static class CartModule
    {
        public static IServiceCollection AddCartModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Data infra services
            var databaseConnectionString = configuration.GetConnectionString("Database");

            services.AddScoped<ISaveChangesInterceptor, EntityAuditInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();
            services.AddDbContext<CartDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(databaseConnectionString);
            });

            return services;
        }

        public static IApplicationBuilder UseCartModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
