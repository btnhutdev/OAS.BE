using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.SeedingData
{
    public static class PermissionSeedingData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, IdPermission = new Guid("C05BDDA5-10BE-4837-8120-CD3E92A1B6B6"), PermissionName = "Auctioneer" },
                new Permission { Id = 2, IdPermission = new Guid("0B747DEC-F3C2-4884-BA2C-AB7A1A2E23BB"), PermissionName = "Bidder" },
                new Permission { Id = 3, IdPermission = new Guid("854C6BCD-6650-44A2-8ED7-343BFD1632D5"), PermissionName = "Admin" });
        }
    }
}
