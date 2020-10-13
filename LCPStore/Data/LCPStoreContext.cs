using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LCPStore.Models;

namespace LCPStore.Data
{
    public class LCPStoreContext : DbContext
    {
        public LCPStoreContext (DbContextOptions<LCPStoreContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(k => new { k.CategoryId, k.ProductId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(p => p.ProductId);
            
            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<ProductOrder>().HasKey(k => new { k.ProductId, k.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(p => p.Product)
                .WithMany(po => po.ProductOrders)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(p => p.Order)
                .WithMany(pc => pc.ProductOrders)
                .HasForeignKey(o => o.OrderId);

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<LCPStore.Models.Account> Account { get; set; }

        public DbSet<LCPStore.Models.Product> Product { get; set; }

        public DbSet<LCPStore.Models.Category> Category { get; set; }

        public DbSet<LCPStore.Models.Cart> Cart { get; set; }
    }
}
