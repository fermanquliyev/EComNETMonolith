using EComNetMonolith.Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EComNetMonolith.Inventory.Data
{
    public class InventoryDbContext: DbContext
    {
        public const string DEFAULT_SCHEMA = "inventory";
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(DEFAULT_SCHEMA);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
