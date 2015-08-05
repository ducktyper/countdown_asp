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

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Discount>().HasRequired(t => t.Product);
        }
    }
}