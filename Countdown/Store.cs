using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Store
    {
        private List<string> barcodes;
        private List<int> prices;

        public Store()
        {
            barcodes = new List<string>();
            prices = new List<int>();
        }

        public void AddItem(string barcode, string name, int price)
        {
            barcodes.Add(barcode);
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
    }
}