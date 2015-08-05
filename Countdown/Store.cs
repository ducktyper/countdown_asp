﻿using Countdown.Models;
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
            Product existing = db.Products.Where(p => p.Barcode == barcode).FirstOrDefault();
            if (existing == null)
                db.Products.Add(new Product() { Barcode = barcode, Name = name, Price = price });
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
            return new Purchase() { Products = GetProducts(barcodes), Discounts = GetDiscounts(barcodes) }.Cost();
        }
        public string PrintReceipt(string[] barcodes)
        {
            return new Purchase() { Products = GetProducts(barcodes), Discounts = GetDiscounts(barcodes) }.PrintReceipt();
        }
        public string Purchase(string[] barcodes)
        {
            Purchase purchase = new Purchase() { Products = GetProducts(barcodes), Discounts = GetDiscounts(barcodes) };
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
            Product product = GetProduct(barcode);
            Discount existing = db.Discounts.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (existing == null)
                db.Discounts.Add(new Discount() { Product = product, Amount = amount });
            else
                existing.Amount = amount;
            db.SaveChanges();
        }
        public void DeleteDiscount(string barcode)
        {
            Discount discount = db.Discounts.Where(p => p.Product.Barcode == barcode).FirstOrDefault();
            db.Discounts.Remove(discount);
            db.SaveChanges();
        }

        private Product[] GetProducts(string[] barcodes)
        {
            return barcodes.Select(b => GetProduct(b)).ToArray();
        }
        private Product GetProduct(string barcode)
        {
            return db.Products.Where(p => p.Barcode == barcode).FirstOrDefault();
        }
        private Discount[] GetDiscounts(string[] barcodes)
        {
            return barcodes.Select(x => GetDiscount(x)).Where(x => x != null).ToArray();
        }
        private Discount GetDiscount(string barcode)
        {
            return db.Discounts.Where(p => p.Product.Barcode == barcode).FirstOrDefault();
        }
    }
}