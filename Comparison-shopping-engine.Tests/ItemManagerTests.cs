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
        public void FindCheapestTest()
        {
            ItemManager m1 = ItemManager.Init();
            m1.ClearList();
            m1.AddItem(new Item("NameA", "Store", 1000, "2017-12-12"));
            m1.AddItem(new Item("NameB", "Store", 1015, "2017-12-12"));
            m1.AddItem(new Item("NameC", "Store", 2015, "2017-12-12"));
            m1.AddItem(new Item("NameA", "Store", 1900, "2017-12-12"));
            Item c = m1.FindCheapest(new Item("NameA"));
            Assert.IsTrue(c.Equals(new Item("NameA", "Store", 1000, "2017-12-12")));
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