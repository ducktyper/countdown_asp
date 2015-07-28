using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Store
    {
        private List<string> barcodes;
        private List<string> names;
        private List<int> prices;

        public Store()
        {
            barcodes = new List<string>();
            names = new List<string>();
            prices = new List<int>();
        }

        public void AddItem(string barcode, string name, int price)
        {
            barcodes.Add(barcode);
            names.Add(name);
            prices.Add(price);
        }

        public int ItemCount()
        {
            return barcodes.Count();
        }

        public int CalculateCost(string[] barcodes)
        {
            int total = 0;
            foreach (string barcode in barcodes)
            {
                int index = Array.IndexOf(barcodes, barcode);
                int price = prices.ElementAt(index);
                total += price;
            }
            return total;
        }

        public string PrintReceipt(string[] barcodes)
        {
            string receipt = "";
            foreach (string barcode in barcodes)
            {
                int index = Array.IndexOf(barcodes, barcode);
                string name = names.ElementAt(index);
                int price = prices.ElementAt(index);
                receipt += String.Format("{0} ${1}{2}", name, price, Environment.NewLine);
            }
            receipt += String.Format("total ${0}", CalculateCost(barcodes));
            return receipt;
        }
    }
}