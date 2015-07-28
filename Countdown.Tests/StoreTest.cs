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
    }
}
