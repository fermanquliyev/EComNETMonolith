using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EComNetMonolith.Cart.Data;

public class CartDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "cart";
    public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
    {
    }

    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema(DEFAULT_SCHEMA);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
