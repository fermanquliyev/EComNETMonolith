using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EComNetMonolith.Shared.Data
{
    public static class Extensions
    {
        public static void UseMigration<T>(this IApplicationBuilder app) where T : DbContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            var dataSeeders = scope.ServiceProvider.GetServices<IDataSeeder>();
            foreach (var seeder in dataSeeders)
            {
                seeder.Seed();
            }
        }
    }
}
