using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("tbl_Category");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Cate__3214EC27A3F93AAE");

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.Property(e => e.IdCategory)
                .IsRequired()
                .HasColumnName("ID_Category");

            builder.HasIndex(e => e.IdCategory, "UQ_Category_ID_Category").IsUnique();

            builder.Property(e => e.CategoryName)
                .HasMaxLength(25)
                .HasColumnName("Category_Name");

            
        }
    }
}
