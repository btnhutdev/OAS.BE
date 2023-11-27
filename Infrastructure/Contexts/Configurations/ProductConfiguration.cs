using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("tbl_Product");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Prod__3214EC27DEA28499");

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.HasIndex(e => e.IdProduct, "UQ_Product_ID_Product").IsUnique();
            
            builder.Property(e => e.CategoryId).HasColumnName("Category_ID");
            builder.Property(e => e.Description).HasMaxLength(500);

            builder.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");

            builder.Property(e => e.IdProduct)
                .IsRequired()
                .HasColumnName("ID_Product");

            builder.Property(e => e.InitPrice).HasColumnName("Init_Price");
            builder.Property(e => e.IsApprove).HasColumnName("Is_Approve");
            builder.Property(e => e.IsPayment).HasColumnName("Is_Payment");
            builder.Property(e => e.IsReject).HasColumnName("Is_Reject");
            builder.Property(e => e.IsSold).HasColumnName("Is_Sold");

            builder.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("Product_Name");

            builder.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");

            builder.Property(e => e.StepPrice).HasColumnName("Step_Price");

            builder.Property(e => e.UserId).HasColumnName("User_ID");

            builder.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.IdCategory)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");

            builder.HasOne(d => d.User).WithMany(p => p.Products)
                .HasPrincipalKey(p => p.IdUser)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Product_User");
        }
    }
}
