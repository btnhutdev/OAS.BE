using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("tbl_Image");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Imag__3214EC27F261D5D6");
            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.HasIndex(e => e.IdImage, "UQ_Image_ID_Image").IsUnique();
            
            builder.Property(e => e.Extension).HasMaxLength(25);
            builder.Property(e => e.IdImage).HasColumnName("ID_Image");
            builder.Property(e => e.IdProduct).HasColumnName("ID_Product");

            builder.Property(e => e.ImageName)
                .HasMaxLength(100)
                .HasColumnName("Image_Name");
            builder.Property(e => e.S3Uri).HasColumnName("S3Uri");

            builder.HasOne(d => d.IdProductNavigation).WithMany(p => p.Images)
                .HasPrincipalKey(p => p.IdProduct)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_Image_Product");
        }
    }
}
