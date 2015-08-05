using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public static void CreateOrUpdate(StoreDB db, string barcode, string name, float price)
        {
            Product existing = db.Products.Where(p => p.Barcode == barcode).FirstOrDefault();
            if (existing == null)
                db.Products.Add(new Product() { Barcode = barcode, Name = name, Price = price });
            else
            {
                existing.Name = name;
                existing.Price = price;
            }
        }

        public string Print()
        {
            return String.Format("{0} ${1:n2}{2}", Name, Price, Environment.NewLine);
        }
    }
}