using System;
using System.Collections;
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
        private List<Discount> discounts;

        public Store()
        {
            products  = new List<Product>();
            purchases = new List<Purchase>();
            discounts = new List<Discount>();
        }
        public void AddItem(string barcode, string name, float price)
        {
            products.Add(new Product(barcode, name,  price));
        }
        public int ItemCount()
        {
            return products.Count();
        }
        public float CalculateCost(string[] barcodes)
        {
            return new Purchase(GetProducts(barcodes), GetDiscounts(barcodes)).Cost();
        }
        public string PrintReceipt(string[] barcodes)
        {
            return new Purchase(GetProducts(barcodes), GetDiscounts(barcodes)).PrintReceipt();
        }
        public string Purchase(string[] barcodes)
        {
            purchases.Add(new Purchase(GetProducts(barcodes), GetDiscounts(barcodes)));
            return PrintReceipt(barcodes);
        }
        public ArrayList PurchaseSummary()
        {
            return new SummaryBuilder(purchases).Generate();
        }
        public void AddDiscount(string barcode, float amount)
        {
            discounts.Add(new Discount(GetProduct(barcode), amount));
        }
        public void DeleteDiscount(string barcode)
        {
            discounts.Remove(GetDiscount(barcode));
        }

        private Product[] GetProducts(string[] barcodes)
        {
            return barcodes.Select(b => GetProduct(b)).ToArray();
        }
        private Product GetProduct(string barcode)
        {
            return products.Find(x => x.Barcode == barcode);
        }
        private Discount[] GetDiscounts(string[] barcodes)
        {
            return barcodes.Select(x => GetDiscount(x)).Where(x => x != null).ToArray();
        }
        private Discount GetDiscount(string barcode)
        {
            return discounts.Find(x => x.Product.Barcode == barcode);
        }
    }
}