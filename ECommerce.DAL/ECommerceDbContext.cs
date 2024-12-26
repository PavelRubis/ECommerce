using ECommerce.Core.Aggregates;
using ECommerce.DAL.Configurations;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }
        public void BeginTransaction()
        {
            Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            Database.RollbackTransaction();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemEntity>().HasData
                (
                    new ItemEntity() { Id = Guid.NewGuid(), Category = "Dress", Price = 10000, Name = "Платье", Code = "20-3333-YY44" },
                    new ItemEntity() { Id = Guid.NewGuid(), Category = "Shoes", Price = 8500, Name = "Туфли", Code = "21-3333-YY44" },
                    new ItemEntity() { Id = Guid.NewGuid(), Category = "Hat", Price = 1000, Name = "Кепка", Code = "22-3333-YY44" },
                    new ItemEntity() { Id = Guid.NewGuid(), Category = "Hat", Price = 7777, Name = "Шляпа 'как-раз'", Code = "23-3333-YY44" },
                    new ItemEntity() { Id = Guid.NewGuid(), Category = "Jeans", Price = 2599.99M, Name = "Джинсы", Code = "24-3333-YY44" }
                );

            var adminAcc = new AccountEntity()
            {
                Id = Guid.NewGuid(),
                Role = "Manager",
                Username = "admin",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("admin")
            };
            var userAcc = new AccountEntity()
            {
                Id = Guid.NewGuid(),
                Role = "Customer",
                Username = "user",
                Password = BCrypt.Net.BCrypt.EnhancedHashPassword("user")
            };
            modelBuilder.Entity<AccountEntity>().HasData
                (
                    adminAcc,
                    userAcc
                );

            var adminCustomer = new CustomerEntity()
            {
                Id = Guid.NewGuid(),
                Name = "admin",
                Code = "0000-2024",
                Discount = 50,
                AccountId = adminAcc.Id,
            };
            var userCustomer = new CustomerEntity()
            {
                Id = Guid.NewGuid(),
                Name = "user",
                Code = "0000-2025",
                Discount = 10,
                AccountId = userAcc.Id, 
            };
            modelBuilder.Entity<CustomerEntity>().HasData
                (
                    userCustomer,
                    adminCustomer
                );
        }
    }
}
