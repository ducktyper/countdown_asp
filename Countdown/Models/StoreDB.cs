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
    }
}