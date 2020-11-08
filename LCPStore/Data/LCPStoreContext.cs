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
            //base.OnModelCreating(modelBuilder);
        }

        public DbSet<LCPStore.Models.Account> Account { get; set; }

        public DbSet<LCPStore.Models.Product> Product { get; set; }

        public DbSet<LCPStore.Models.Category> Category { get; set; }

        public DbSet<LCPStore.Models.Cart> Cart { get; set; }

        public DbSet<LCPStore.Models.Contact> Contact { get; set; }

        public DbSet<LCPStore.Models.CartItem> CartItem { get; set; }

        public DbSet<LCPStore.Models.Order> Order { get; set; }
        public DbSet<LCPStore.Models.OrderItem> OrderItem { get; set; }
    }
}
