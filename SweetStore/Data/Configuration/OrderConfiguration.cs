using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweetStore.Model.Order;

namespace SweetStore.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>,
                                      IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.HasIndex(x => new { x.Id });

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.HasMany(x => x.Items)
                   .WithOne(x => x.Order)
                   .HasForeignKey(x => x.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(x => x.OrderId );
            builder.HasOne(x => x.Product)
                   .WithMany()
                   .HasForeignKey(x => x.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
