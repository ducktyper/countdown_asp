using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Countdown.Models;
using System.Data.Entity;

namespace Countdown.Tests
{
    public class AssertHelper
    {
        public static string ToString(string[] array)
        {
            return "[" + String.Join(",", array.Select(v => "'" + v + "'")) + "]";
        }
        public static string ToString(string[][] jaggedArray)
        {
            return "[" + String.Join(",", jaggedArray.Select(a => ToString(a))) + "]";
        }
    }
    
    [TestClass]
    public class StoreTest
    {
        Store store;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<StoreDB>());
        }

        [TestInitialize]
        public void InitStoreHavingTwoItems()
        {
            new StoreDB().Database.ExecuteSqlCommand(
                "DELETE FROM Products; DELETE FROM Discounts; DELETE FROM Purchases");
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
        public void TestAddItemOverrideDuplication()
        {
            store.AddItem("0001", "jazz apple", 20);
            string expected = String.Format("jazz apple $20.00{0}total $20.00", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001"}));
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
            store.Purchase(new string[] {"0001"});
            string time = DateTime.Now.ToString("dd MM yyyy").Replace(" ", "/");
            string expected = String.Format("[['Time','Number of Products','Cost'],['{0}','1','5']]", time);
            Assert.AreEqual(expected, AssertHelper.ToString(store.PurchaseSummary()));
        }

        [TestMethod]
        public void TestPurchaseItemTwice()
        {
            store.Purchase(new string[] {"0001"});
            store.Purchase(new string[] {"0001"});
            Assert.AreEqual(1 + 2, store.PurchaseSummary().Length);
        }

        [TestMethod]
        public void TestPurchaseWithSameDiscountTwice()
        {
            store.AddDiscount("0001", 1);
            store.Purchase(new string[] {"0001"});
            store.Purchase(new string[] {"0001"});
            Assert.AreEqual("4", store.PurchaseSummary()[1][2]);
            Assert.AreEqual("4", store.PurchaseSummary()[2][2]);
        }

        [TestMethod]
        public void TestAddDiscount()
        {
            store.AddDiscount("0001", 1);
            string expected = String.Format("apple $5.00{0}apple -$1.00{0}total $4.00", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001"}));
        }

        [TestMethod]
        public void TestAddDiscountOverrideDuplication()
        {
            store.AddDiscount("0001", 1);
            store.AddDiscount("0001", 2);
            string expected = String.Format("apple $5.00{0}apple -$2.00{0}total $3.00", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001"}));
        }

        [TestMethod]
        public void TestDeleteDiscount()
        {
            store.AddDiscount("0001", 1);
            store.DeleteDiscount("0001");
            string expected = String.Format("apple $5.00{0}total $5.00", Environment.NewLine);
            Assert.AreEqual(expected, store.PrintReceipt(new string[] {"0001"}));
        }
    }
}
