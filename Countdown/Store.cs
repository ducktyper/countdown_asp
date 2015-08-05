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
        private StoreDB db;

        public Store()
        {
            db = new StoreDB();
        }
        public void AddItem(string barcode, string name, float price)
        {
            Product.CreateOrUpdate(db, barcode, name, price);
            db.SaveChanges();
        }
        public int ItemCount()
        {
            return db.Products.Count();
        }
        public float CalculateCost(string[] barcodes)
        {
            return BuildPurchase(barcodes).Cost();
        }
        public string PrintReceipt(string[] barcodes)
        {
            return BuildPurchase(barcodes).PrintReceipt();
        }
        public string Purchase(string[] barcodes)
        {
            Purchase purchase = BuildPurchase(barcodes);
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return purchase.PrintReceipt();
        }
        public string[][] PurchaseSummary()
        {
            return new SummaryBuilder(db.Purchases.ToList()).Generate();
        }
        public void AddDiscount(string barcode, float amount)
        {
            Discount.CreateOrUpdate(db, barcode, amount);
            db.SaveChanges();
        }
        public void DeleteDiscount(string barcode)
        {
            db.Discounts.Remove(Discount.FindByBarcode(db, barcode));
            db.SaveChanges();
        }

        private Purchase BuildPurchase(string[] barcodes)
        {
            return Models.Purchase.Build(db, barcodes);
        }
    }
}