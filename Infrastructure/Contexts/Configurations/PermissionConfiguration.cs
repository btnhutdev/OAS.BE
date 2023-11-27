using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Contexts.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("tbl_Permission");

            builder.HasKey(e => e.Id).HasName("PK__tbl_Perm__3214EC2712A75102");

            builder.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();

            builder.HasIndex(e => e.IdPermission, "UQ_Permission_ID_Permission").IsUnique();
            
            builder.Property(e => e.IdPermission).HasColumnName("ID_Permission");

            builder.Property(e => e.PermissionName)
                .HasMaxLength(10)
                .HasColumnName("Permission_Name");
        }
    }
}
