using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SweetStore.Model.Products;

namespace SweetStore.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Price)
                   .IsRequired();

            // العلاقة مع Category
            builder.HasOne(p => p.Category)       // كل Product له Category
                   .WithMany(c => c.Products)    // كل Category لها مجموعة Products
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // لو حذفنا الفئة، تحذف المنتجات المرتبطة
        }
    }
}
