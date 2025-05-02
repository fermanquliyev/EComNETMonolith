using EComNetMonolith.Inventory.Data;
using EComNetMonolith.Inventory.Data.Seed;
using EComNetMonolith.Shared.CQRS.Behaviors;
using EComNetMonolith.Shared.Data;
using EComNetMonolith.Shared.Data.Interceptors;
using EComNetMonolith.Shared.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EComNetMonolith.Inventory
{
    public static class InventoryModule
    {
        public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            //Data infra services
            var databaseConnectionString = configuration.GetConnectionString("Database");

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            });

            services.AddScoped<ISaveChangesInterceptor, EntityAuditInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();

            services.AddDbContext<InventoryDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(databaseConnectionString);
            });

            services.AddScoped<IDataSeeder, InventoryDataSeeder>();

            return services;
        }

        public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
        {
            app.UseMigration<InventoryDbContext>();
            app.AddEndpoints(typeof(InventoryModule).Assembly);
            return app;
        }
    }
}
