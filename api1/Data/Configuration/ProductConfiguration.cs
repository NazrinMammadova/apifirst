using api1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api_Intro.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p=>p.SalePrice).IsRequired().HasDefaultValue(10);
            builder.Property(p=>p.SalePrice).IsRequired().HasDefaultValue(10);
            builder.Property(p => p.CreateDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.DeleteDate).HasDefaultValueSql("GetUtcDate()");
        }
    }
}
