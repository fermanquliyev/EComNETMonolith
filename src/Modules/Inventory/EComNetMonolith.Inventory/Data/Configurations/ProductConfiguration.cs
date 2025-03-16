using EComNetMonolith.Inventory.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EComNetMonolith.Inventory.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(128);

            builder.Property(p => p.Description).HasMaxLength(512);

            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

            builder.Property(p => p.Stock).IsRequired();

            builder.Property(p => p.ImageUrl).HasMaxLength(256).IsRequired();

            builder.Property(p => p.Categories).IsRequired();
        }
    }
}
