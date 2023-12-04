using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace Infrastructure.SeedingData
{
    public static class UserSeedingData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    IdUser = Guid.NewGuid(),
                    Username = "admin",
                    Password = "123",
                    PhoneNumber = "0111222333",
                    Email = "your email",
                    FirstName = "Cam",
                    LastName = "Nguyen Van",
                    Gender = false,
                    IdPermission = new Guid("854C6BCD-6650-44A2-8ED7-343BFD1632D5")
                },
                 new User
                 {
                     Id = 2,
                     IdUser = Guid.NewGuid(),
                     Username = "bid01",
                     Password = "123",
                     PhoneNumber = "0222333444",
                     Email = "your email",
                     FirstName = "Huong",
                     LastName = "Nguyen Thi",
                     Gender = false,
                     IdPermission = new Guid("0B747DEC-F3C2-4884-BA2C-AB7A1A2E23BB")
                 },
                 new User
                 {
                     Id = 3,
                     IdUser = Guid.NewGuid(),
                     Username = "bid02",
                     Password = "123",
                     PhoneNumber = "0222333444",
                     Email = "your email",
                     FirstName = "Huynh",
                     LastName = "Tran Van",
                     Gender = false,
                     IdPermission = new Guid("0B747DEC-F3C2-4884-BA2C-AB7A1A2E23BB")
                 },
                 new User
                 {
                     Id = 4,
                     IdUser = Guid.NewGuid(),
                     Username = "auc01",
                     Password = "123",
                     PhoneNumber = "0123456789",
                     Email = "your email",
                     FirstName = "Hung",
                     LastName = "Le Van",
                     Gender = true,
                     IdPermission = new Guid("C05BDDA5-10BE-4837-8120-CD3E92A1B6B6")
                 },
                new User
                {
                    Id = 5,
                    IdUser = Guid.NewGuid(),
                    Username = "auc02",
                    Password = "123",
                    PhoneNumber = "0123456789",
                    Email = "your email",
                    FirstName = "Hung",
                    LastName = "Le Van",
                    Gender = true,
                    IdPermission = new Guid("C05BDDA5-10BE-4837-8120-CD3E92A1B6B6")
                });
        }
    }
}
