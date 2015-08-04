using Countdown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Purchase
    {
        public Product[] products;
        public Discount[] discounts;
        public DateTime Purchased_at { get; private set; }

        public Purchase(Product[] _products, Discount[] _discounts)
        {
            products     = _products;
            discounts    = _discounts;
            Purchased_at = DateTime.Now;
        }
        public string DisplayTime()
        {
            return String.Format("{0:MM dd YYYY}", Purchased_at);
        }
        public int ProductCount()
        {
            return products.Count();
        }
        public float Cost()
        {
            return products.Select(b => b.Price).Sum() - discounts.Select(d => d.Amount).Sum();
        }
        public string PrintReceipt()
        {
            return PrintEach() + PrintDiscounts() + PrintTotal();
        }

        private string PrintEach()
        {
            return products.Aggregate("", (str, p) => str + p.Print());
        }
        private string PrintDiscounts()
        {
            return discounts.Aggregate("", (str, d) => str + d.Print());
        }
        private string PrintTotal()
        {
            return String.Format("total ${0:n2}", Cost());
        }
    }
}