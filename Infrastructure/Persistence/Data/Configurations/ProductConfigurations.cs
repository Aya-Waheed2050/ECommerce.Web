using Domain.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(e => e.ProductType)
                   .WithMany()
                   .HasForeignKey(e => e.TypeId);

            builder.HasOne(e => e.ProductBrand)
                   .WithMany()
                   .HasForeignKey(e => e.BrandId);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,3)");

        }
    }
}
