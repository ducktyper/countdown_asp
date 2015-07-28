﻿using System;
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
            return GetProducts(barcodes).Select(b => b.price).Sum();
        }

        public string PrintReceipt(string[] barcodes)
        {
            return PrintEach(barcodes) + PrintTotal(barcodes);
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
            return String.Format("{0} ${1}{2}", p.name, p.price, Environment.NewLine);
        }

        private string PrintTotal(string[] barcodes)
        {
            return String.Format("total ${0}", CalculateCost(barcodes));
        }
    }
}