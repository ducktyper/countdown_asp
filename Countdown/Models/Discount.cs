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

        public static void CreateOrUpdate(StoreDB db, string barcode, float amount)
        {
            Product product = Product.FindByBarcode(db, barcode);
            Discount existing = db.Discounts.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (existing == null)
                db.Discounts.Add(new Discount() { Product = product, Amount = amount });
            else
                existing.Amount = amount;
        }
        public static Discount[] FindByBarcodes(StoreDB db, string[] barcodes)
        {
            return barcodes.Select(x => FindByBarcode(db, x)).Where(x => x != null).ToArray();
        }
        public static Discount FindByBarcode(StoreDB db, string barcode)
        {
            return db.Discounts.Where(p => p.Product.Barcode == barcode).FirstOrDefault();
        }

        public string Print()
        {
            return String.Format("{0} -${1:n2}{2}", Product.Name, Amount, Environment.NewLine);
        }
    }
}