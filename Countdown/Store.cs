using Countdown.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Store
    {
        private List<Purchase> purchases;
        private Dictionary<string, Discount> discounts;
        private StoreDB db;

        public Store()
        {
            purchases = new List<Purchase>();
            discounts = new Dictionary<string, Discount>();
            db        = new StoreDB();
        }
        public void AddItem(string barcode, string name, float price)
        {
            Product existing = db.Products.Where(p => p.Barcode == barcode).FirstOrDefault();
            if (existing == null)
            {
                Product product = new Product() { Barcode = barcode, Name = name, Price = price };
                db.Products.Add(product);
            }
            else
            {
                existing.Name = name;
                existing.Price = price;
            }
            db.SaveChanges();
        }
        public int ItemCount()
        {
            return db.Products.Count();
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
        public string[][] PurchaseSummary()
        {
            return new SummaryBuilder(purchases).Generate();
        }
        public void AddDiscount(string barcode, float amount)
        {
            discounts[barcode] = new Discount(GetProduct(barcode), amount);
        }
        public void DeleteDiscount(string barcode)
        {
            discounts.Remove(barcode);
        }

        private Product[] GetProducts(string[] barcodes)
        {
            return barcodes.Select(b => GetProduct(b)).ToArray();
        }
        private Product GetProduct(string barcode)
        {
            return new StoreDB().Products.Where(p => p.Barcode == barcode).FirstOrDefault();
        }
        private Discount[] GetDiscounts(string[] barcodes)
        {
            return barcodes.Select(x => GetDiscount(x)).Where(x => x != null).ToArray();
        }
        private Discount GetDiscount(string barcode)
        {
            return discounts.ContainsKey(barcode) ? discounts[barcode] : null;
        }
    }
}