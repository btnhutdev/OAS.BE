using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.SeedingData
{
    public static class CategorySeedingData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, IdCategory = Guid.NewGuid(), CategoryName = "IPhone" },
                new Category { Id = 2, IdCategory = Guid.NewGuid(), CategoryName = "SamSung" },
                new Category { Id = 3, IdCategory = Guid.NewGuid(), CategoryName = "Realme" },
                new Category { Id = 4, IdCategory = Guid.NewGuid(), CategoryName = "Xiaomi" },
                new Category { Id = 5, IdCategory = Guid.NewGuid(), CategoryName = "Oppo" }
                );
        }
    }
}
