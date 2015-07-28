using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Countdown.Tests
{
    [TestClass]
    public class StoreTest
    {
        [TestMethod]
        public void TestAddItem()
        {
            Store store = new Store();
            store.AddItem("0001", "apple", 10);
            Assert.AreEqual(1, store.ItemCount());
        }

        [TestMethod]
        public void TestAddItemInceasesItemCount()
        {
            Store store = new Store();
            store.AddItem("0001", "apple", 10);
            store.AddItem("0002", "orange", 10);
            Assert.AreEqual(2, store.ItemCount());
        }

        [TestMethod]
        public void TestCalculateCost()
        {
            Store store = new Store();
            store.AddItem("0001", "apple", 5);
            store.AddItem("0002", "orange", 10);
            Assert.AreEqual(15, store.CalculateCost(new string[] {"0001", "0002"}));
        }
    }
}
