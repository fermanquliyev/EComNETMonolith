using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.Data.Seed;
using EComNetMonolith.Shared.Data;
using EComNetMonolith.Shared.Data.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EComNetMonolith.Inventory
{
    public static class InventoryModule
    {
        public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Data infra services
            var databaseConnectionString = configuration.GetConnectionString("Database");

            services.AddScoped<EntityAuditInterceptor>();

            services.AddDbContext<InventoryDbContext>((sp,options) =>
            {
                options.AddInterceptors(sp.GetRequiredService<EntityAuditInterceptor>());
                options.UseNpgsql(databaseConnectionString);
            });

            services.AddScoped<IDataSeeder, InventoryDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
        {
            app.UseMigration<InventoryDbContext>();
            return app;
        }
    }
}
