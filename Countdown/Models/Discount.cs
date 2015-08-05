using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public virtual Product Product { get; set; }
        public float Amount { get; set; }

        public static void CreateOrUpdate(StoreDB db, Product product, float amount)
        {
            Discount existing = db.Discounts.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (existing == null)
                db.Discounts.Add(new Discount() { Product = product, Amount = amount });
            else
                existing.Amount = amount;
        }

        public string Print()
        {
            return String.Format("{0} -${1:n2}{2}", Product.Name, Amount, Environment.NewLine);
        }
    }
}