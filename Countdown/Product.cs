using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Product
    {
        public string Barcode { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public Product(string barcode, string name, float price)
        {
            Barcode = barcode;
            Name    = name;
            Price   = price;
        }
        public string Print()
        {
            return String.Format("{0} ${1:n2}{2}", Name, Price, Environment.NewLine);
        }
    }
}