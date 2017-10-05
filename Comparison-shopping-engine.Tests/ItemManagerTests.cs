using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine.Tests
{
    [TestClass()]
    public class ItemManagerTests
    {
        [TestMethod()]
        public void InitTest()
        {
            ItemManager m1 = ItemManager.Init();
            ItemManager m2 = ItemManager.Init();
            Assert.AreEqual(m1, m2);
        }

        [TestMethod()]
        public void AddItemTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.ClearList();
            Item a = new Item("Name", "Store", 1515, DateTime.Now);
            m1.AddItem(a);
            Item b = m1.FindCheapest(a);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod()]
        public void CompareAddItemTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.ClearList();
            Item a = new Item("Name", "StoreA", 1515, DateTime.Now);
            Item b = new Item("Name", "StoreB", 915, DateTime.Now);
            m1.CompareAddItem(a);
            m1.CompareAddItem(a);
            m1.CompareAddItem(b);
            Assert.IsTrue(m1.Count() == 2);
        }

        [TestMethod()]
        public void FindCheaperTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.ClearList();
            Item a = new Item("Name", "StoreA", 1515, DateTime.Now);
            Item b = new Item("Name", "StoreB", 1099, DateTime.Now);
            m1.CompareAddItem(a);
            m1.CompareAddItem(b);
            Item c = m1.FindCheapest(a);
            Assert.IsTrue(c.Equals(b));
        }

        [TestMethod()]
        public void CountTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.ClearList();
            Item a = new Item("Name", "Store", 999, "2017-10-05");
            m1.AddItem(a);
            Assert.IsTrue(m1.Count() == 1);
        }

        [TestMethod()]
        public void ClearListTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.ClearList();
            Assert.IsTrue(m1.Count() == 0);
        }
    }
}