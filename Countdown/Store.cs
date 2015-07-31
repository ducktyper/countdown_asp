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

    public class Purchase
    {
        public Product[] products {get; private set;}
        public DateTime purchased_at {get; private set;}

        public Purchase(Product[] _products)
        {
            products     = _products;
            purchased_at = DateTime.Now;
        }
        public string DisplayTime()
        {
            return String.Format("{0:MM dd YYYY}", purchased_at);
        }
        public int ProductCount()
        {
            return products.Count();
        }
        public float Cost()
        {
            return products.Select(b => b.price).Sum();
        }
    }

    public class SummaryBuilder
    {
        private ArrayList summary;
        private List<Purchase> purchases;

        public SummaryBuilder(List<Purchase> _purchases)
        {
            summary = new ArrayList();
            purchases = _purchases;
        }
        public ArrayList Generate()
        {
            AddHeaders();
            AddPurchases();
            return summary;
        }

        private void AddPurchases()
        {
            purchases.ForEach(p => AddPurchase(p));
        }
        private void AddPurchase(Purchase p)
        {
            AddData(p.DisplayTime(), p.ProductCount(), p.Cost());
        }
        private void AddData(string time, int count,  float cost)
        {
            summary.Add(new string[] {time, count.ToString(), cost.ToString()});
        }
        private void AddHeaders()
        {
            summary.Add(new string[] {"Time", "Number of Products", "Cost"});
        }
    }

    public class Store
    {
        private List<Product> products;
        private List<Purchase> purchases;

        public Store()
        {
            products  = new List<Product>();
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
            purchases.Add(new Purchase(GetProducts(barcodes)));
            return PrintReceipt(barcodes);
        }
        public ArrayList PurchaseSummary()
        {
            return new SummaryBuilder(purchases).Generate();
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