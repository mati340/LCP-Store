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

            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<LCPStore.Models.Account> Account { get; set; }

        public DbSet<LCPStore.Models.Product> Product { get; set; }

        public DbSet<LCPStore.Models.Category> Category { get; set; }

        public DbSet<LCPStore.Models.Cart> Cart { get; set; }

        public DbSet<LCPStore.Models.Contact> Contact { get; set; }
    }
}
