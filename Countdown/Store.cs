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

    public struct Discount
    {
        public string barcode;
        public float amount;
    }

    public class Purchase
    {
        public Product[] products;
        public Discount[] discounts;
        public DateTime Purchased_at {get; private set;}

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
            return products.Select(b => b.price).Sum() - discounts.Select(d => d.amount).Sum();
        }
        public string PrintReceipt()
        {
            return PrintEach() + PrintDiscounts() + PrintTotal();
        }

        private string PrintEach()
        {
            return products.Aggregate("", (str, p) => str + PrintItem(p));
        }
        private string PrintDiscounts()
        {
            return discounts.Aggregate("", (str, d) => str + PrintDiscount(d));
        }
        private string PrintDiscount(Discount d)
        {
            Product p = Array.Find(products, x => x.barcode == d.barcode);
            return String.Format("{0} -${1:n2}{2}", p.name, d.amount, Environment.NewLine);
        }
        private string PrintItem(Product p)
        {
            return String.Format("{0} ${1:n2}{2}", p.name, p.price, Environment.NewLine);
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
            products.Add(new Product() { barcode = barcode, name = name, price = price });
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
            discounts.Add(new Discount() { barcode = barcode, amount = amount });
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
            return products.Find(x => x.barcode == barcode);
        }
        private Discount[] GetDiscounts(string[] barcodes)
        {
            return barcodes.Select(x => GetDiscount(x)).Where(x => x.barcode != null).ToArray();
        }
        private Discount GetDiscount(string barcode)
        {
            return discounts.Find(x => x.barcode == barcode);
        }
    }
}