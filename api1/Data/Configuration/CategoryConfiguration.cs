using api1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api1.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c=>c.Name).IsRequired().HasMaxLength(50);
            builder.Property(c=>c.CreateDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(c=>c.DeleteDate).HasDefaultValueSql("GetUtcDate()");
        }
    }
}
