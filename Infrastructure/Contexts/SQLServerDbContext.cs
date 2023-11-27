using Domain.Entities;
using Infrastructure.Contexts.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Contexts
{
    public class SQLServerDbContext : DbContext
    {
        public SQLServerDbContext(DbContextOptions<SQLServerDbContext> options) : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }

        public virtual DbSet<Auction> Auctions { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<DetailAuction> DetailAuctions { get; set; }

        public virtual DbSet<HistoryPayment> HistoryPayments { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AuctionConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DetailAuctionConfiguration());
            modelBuilder.ApplyConfiguration(new HistoryPaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            var seedMethods = GetType().Assembly.GetTypes()
                                    .Where(t => t.Namespace == "Infrastructure.SeedingData")
                                    .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static)

            .Where(m => m.Name == "Seed" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(ModelBuilder)));

            foreach (var seedMethod in seedMethods)
            {
                seedMethod.Invoke(null, new object[] { modelBuilder });
            }
        }
    }
}
