using Countdown.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Discount
    {
        public Product Product { get; set; }
        public float Amount { get; set; }

        public Discount(Product product, float amount)
        {
            Product = product;
            Amount = amount;
        }
        public string Print()
        {
            return String.Format("{0} -${1:n2}{2}", Product.Name, Amount, Environment.NewLine);
        }
    }
}