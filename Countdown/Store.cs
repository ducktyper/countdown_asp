using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public struct Product
    {
        public string barcode;
        public string name;
        public float price;
    }
    public struct Purchase
    {
        public Product[] products;
        public DateTime purchased_at;
    }

    public class Store
    {
        private List<Product> products;
        private List<Purchase> purchases;

        public Store()
        {
            products = new List<Product>();
            purchases = new List<Purchase>();
        }

        public void AddItem(string barcode, string name, float price)
        {
            products.Add(new Product() { barcode = barcode, name = name, price = price });
        }

        public int ItemCount()
        {
            return products.Count();
        }

        public float CalculateCost(string[] barcodes)
        {
            return GetProducts(barcodes).Select(b => b.price).Sum();
        }

        public string PrintReceipt(string[] barcodes)
        {
            return PrintEach(barcodes) + PrintTotal(barcodes);
        }

        public string Purchase(string[] barcodes)
        {
            purchases.Add(new Purchase() {
                products     = GetProducts(barcodes),
                purchased_at = DateTime.Now
            }); 
            return PrintReceipt(barcodes);
        }

        public ArrayList PurchaseSummary()
        {
            ArrayList summary = new ArrayList();
            summary.Add(new string[] {"Time", "Number of Products", "Cost"});
            foreach (Purchase p in purchases)
            {
                string time = String.Format("{0:MM dd YYYY}", p.purchased_at);
                string count = "" + p.products.Count();
                string sum = "1" + p.products.Select(b => b.price).Sum();
                summary.Add(new string[] { time, count, sum });
            }
            return summary;
        }

        private Product[] GetProducts(string[] barcodes)
        {
            return barcodes.Select(b => GetProduct(b)).ToArray();
        }

        private Product GetProduct(string barcode)
        {
            return products.Find(x => x.barcode == barcode);
        }

        private string PrintEach(string[] barcodes)
        {
            return GetProducts(barcodes).Aggregate("", (str, p) => str + PrintItem(p));
        }

        private string PrintItem(Product p)
        {
            return String.Format("{0} ${1:n2}{2}", p.name, p.price, Environment.NewLine);
        }

        private string PrintTotal(string[] barcodes)
        {
            return String.Format("total ${0:n2}", CalculateCost(barcodes));
        }
    }
}