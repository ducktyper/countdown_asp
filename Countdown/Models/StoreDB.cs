using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class StoreDB : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Discount>().HasRequired(t => t.Product);
            modelBuilder.Entity<Purchase>().HasMany(t => t.Products).WithMany();
            modelBuilder.Entity<Purchase>().HasMany(t => t.Discounts).WithMany();
        }
    }
}