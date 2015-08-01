using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countdown
{
    public class SummaryBuilder
    {
        private List<string[]> summary;
        private List<Purchase> purchases;

        public SummaryBuilder(List<Purchase> _purchases)
        {
            summary = new List<string[]>();
            purchases = _purchases;
        }
        public string[][] Generate()
        {
            AddHeaders();
            AddPurchases();
            return summary.ToArray();
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
}