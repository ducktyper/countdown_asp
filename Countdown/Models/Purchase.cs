using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public DateTime Purchased_at { get; private set; }

        public static Purchase Build(StoreDB db, string[] barcodes)
        {
            return new Purchase() {
                Products = Product.FindByBarcodes(db, barcodes),
                Discounts = Discount.FindByBarcodes(db, barcodes)
            };
        }

        public Purchase()
        {
            Purchased_at = DateTime.Now;
        }
        public string DisplayTime()
        {
            return Purchased_at.ToString("dd MM yyyy").Replace(" ", "/");
        }
        public int ProductCount()
        {
            return Products.Count();
        }
        public float Cost()
        {
            return Products.Select(b => b.Price).Sum() - Discounts.Select(d => d.Amount).Sum();
        }
        public string PrintReceipt()
        {
            return PrintEach() + PrintDiscounts() + PrintTotal();
        }

        private string PrintEach()
        {
            return Products.Aggregate("", (str, p) => str + p.Print());
        }
        private string PrintDiscounts()
        {
            return Discounts.Aggregate("", (str, d) => str + d.Print());
        }
        private string PrintTotal()
        {
            return String.Format("total ${0:n2}", Cost());
        }
    }
}