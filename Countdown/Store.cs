using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public struct Product
    {
        public string barcode;
        public string name;
        public int price;
    }

    public class Store
    {
        private List<Product> products;

        public Store()
        {
            products = new List<Product>();
        }

        public void AddItem(string barcode, string name, int price)
        {
            products.Add(new Product() { barcode = barcode, name = name, price = price });
        }

        public int ItemCount()
        {
            return products.Count();
        }

        public int CalculateCost(string[] barcodes)
        {
            return barcodes.Aggregate(0, (sum, barcode) => sum + GetProduct(barcode).price);
        }

        public string PrintReceipt(string[] barcodes)
        {
            string receipt = "";
            foreach (string barcode in barcodes)
            {
                Product p = products.Find(x => x.barcode == barcode);
                receipt += String.Format("{0} ${1}{2}", p.name, p.price, Environment.NewLine);
            }
            receipt += String.Format("total ${0}", CalculateCost(barcodes));
            return receipt;
        }

        private Product GetProduct(string barcode)
        {
            return products.Find(x => x.barcode == barcode);
        }
    }
}