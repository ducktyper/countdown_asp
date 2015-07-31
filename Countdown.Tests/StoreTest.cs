﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace Countdown.Tests
{
    [TestClass]
    public class StoreTest
    {
        Store store;

        [TestInitialize]
        public void InitStoreHavingTwoItems()
        {
            store = new Store();
            store.AddItem("0001", "apple", 5);
            store.AddItem("0002", "orange", 10);
        }

        [TestMethod]
        public void TestAddItem()
        {
            Assert.AreEqual(2, store.ItemCount());
        }

        [TestMethod]
        public void TestCalculateCost()
        {
            Assert.AreEqual(15, store.CalculateCost(new string[] {"0001", "0002"}));
        }

        [TestMethod]
        public void TestPrintReceipt()
        {
            string expected = String.Format("apple $5.00{0}orange $10.00{0}total $15.00", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001", "0002"}));
        }

        [TestMethod]
        public void TestPurchase()
        {
            string expected = String.Format("apple $5.00{0}orange $10.00{0}total $15.00", Environment.NewLine);
            Assert.AreEqual(expected, store.Purchase(new string[] {"0001", "0002"}));
        }

        [TestMethod]
        public void TestPurchaseSummary()
        {
            string time = String.Format("{0:MM dd YYYY}", DateTime.Now);
            ArrayList expected = new ArrayList();
            expected.Add(new string[] { "Time", "Number of Products", "Cost" });
            expected.Add(new string[] {time, "1", "5"});
            CollectionAssert.Equals(expected, store.PurchaseSummary());
        }
    }
}
