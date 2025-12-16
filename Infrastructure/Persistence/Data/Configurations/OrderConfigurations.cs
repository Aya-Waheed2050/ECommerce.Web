using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Order = Domain.Models.OrderModule.Order;
using Domain.Models.OrderModule;

namespace Persistence.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(O => O.SubTotal)
                   .HasColumnType("Decimal(18,3)");

            builder.HasMany(O => O.Items)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(O => O.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull);

            builder.OwnsOne(O => O.ShipToAddress , sh => sh.WithOwner());

            builder.Property(o => o.Status)
                   .HasConversion(ps => ps.ToString() ,ps => Enum.Parse<OrderPaymentStatus>(ps));
        }
    }
}
