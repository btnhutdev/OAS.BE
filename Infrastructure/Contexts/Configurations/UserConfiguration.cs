using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("tbl_User");

            builder.HasKey(e => e.Id).HasName("PK__tbl_User__3214EC27A0DFF69B");

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.HasIndex(e => e.IdUser, "UQ_User_ID_User").IsUnique();

            builder.Property(e => e.IdUser).HasColumnName("ID_User");

            builder.Property(e => e.Email).HasMaxLength(255);

            builder.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("First_Name");

            builder.Property(e => e.IdPermission).HasColumnName("ID_Permission");

            builder.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("Last_Name");

            builder.Property(e => e.Password).HasMaxLength(500);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("Phone_Number");

            builder.Property(e => e.Username).HasMaxLength(50);

            builder.HasOne(d => d.IdPermissionNavigation).WithMany(p => p.Users)
                .HasPrincipalKey(p => p.IdPermission)
                .HasForeignKey(d => d.IdPermission)
                .HasConstraintName("FK_User_Permission");
        }
    }
}
