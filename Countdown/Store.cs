using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class Store
    {
        private List<string> barcodes;

        public Store()
        {
            barcodes = new List<string>();
        }

        public void AddItem(string barcode, string name, int price)
        {
            barcodes.Add(barcode);
        }

        public int ItemCount()
        {
            return barcodes.Count();
        }
    }
}