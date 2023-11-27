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
                new Category { Id = 1, IdCategory = Guid.NewGuid(), CategoryName = "Smart Phone" },
                new Category { Id = 2, IdCategory = Guid.NewGuid(), CategoryName = "Laptop" },
                new Category { Id = 3, IdCategory = Guid.NewGuid(), CategoryName = "Furniture" },
                new Category { Id = 4, IdCategory = Guid.NewGuid(), CategoryName = "Motorbike" }
                );
        }
    }
}
