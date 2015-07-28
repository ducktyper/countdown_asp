using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            string expected = String.Format("apple $5{0}orange $10{0}total $15", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001", "0002"}));
        }
    }
}
