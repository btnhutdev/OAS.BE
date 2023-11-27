using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class HistoryPaymentConfiguration : IEntityTypeConfiguration<HistoryPayment>
    {
        public void Configure(EntityTypeBuilder<HistoryPayment> builder)
        {
            builder.ToTable("tbl_History_Payment");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Hist__3214EC2706318AA6");
            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.HasIndex(e => e.IdHistoryPayment, "UQ_History_Payment_ID_History_Payment").IsUnique();

            builder.Property(e => e.DatePayment)
                .HasColumnType("datetime")
                .HasColumnName("Date_Payment");

            builder.Property(e => e.Email).HasMaxLength(250);
            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");

            builder.Property(e => e.IdHistoryPayment).HasColumnName("ID_History_Payment");
            builder.Property(e => e.IdProduct).HasColumnName("ID_Product");
            builder.Property(e => e.IdUser).HasColumnName("ID_User");

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");

            builder.Property(e => e.OrderNotes)
                .HasMaxLength(250)
                .HasColumnName("Order_Notes");

            builder.Property(e => e.OrderType)
                .HasMaxLength(25)
                .HasColumnName("Order_Type");

            builder.Property(e => e.ShipingAddress)
                .HasMaxLength(200)
                .HasColumnName("Shiping_Address");

            builder.Property(e => e.Telephone).HasMaxLength(10);
            builder.Property(e => e.TotalPrice).HasColumnName("Total_Price");

            builder.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .HasColumnName("ZIP_Code");

            builder.HasOne(d => d.IdProductNavigation).WithMany(p => p.HistoryPayments)
                .HasPrincipalKey(p => p.IdProduct)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_History_Payment_Product");

            builder.HasOne(d => d.IdUserNavigation).WithMany(p => p.HistoryPayments)
                .HasPrincipalKey(p => p.IdUser)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_History_Payment_User");
        }
    }
}
