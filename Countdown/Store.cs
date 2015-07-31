using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
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