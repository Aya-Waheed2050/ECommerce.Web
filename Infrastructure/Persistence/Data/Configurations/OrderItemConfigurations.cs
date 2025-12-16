using Domain.Models.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(OI => OI.Price).HasColumnType("Decimal(18,3)");
            builder.OwnsOne(OI => OI.Product ,p => p.WithOwner());
        }
    }
}
