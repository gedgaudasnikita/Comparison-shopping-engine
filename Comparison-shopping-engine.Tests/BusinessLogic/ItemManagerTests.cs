using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace Comparison_shopping_engine.Tests
{
    [TestClass()]
    public class ItemManagerTests
    {
        [TestMethod()]
        public void InitTest()
        {
            ItemManager m1 = ItemManager.getInstance();
            ItemManager m2 = ItemManager.getInstance();
            Assert.AreEqual(m1, m2);
        }

        [TestMethod()]
        public void AddItemTest()
        {
            ItemManager m1 = ItemManager.getInstance();
            m1.ClearList();
            Item a = new Item("Name", "Store", 1515, DateTime.Now);
            m1.AddItem(a);
            Item b = m1.FindCheapest(a);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod()]
        public void ExistsTest()
        {
            ItemManager m1 = ItemManager.getInstance();
            m1.ClearList();
            Item a = new Item("Name", "StoreA", 1515, DateTime.Now);
            Item b = new Item("Name", "StoreB", 915, DateTime.Now);
            if (!m1.Exists(a)) m1.AddItem(a);
            if (!m1.Exists(a)) m1.AddItem(a);
            if (!m1.Exists(a)) m1.AddItem(a);
            if (!m1.Exists(a)) m1.AddItem(a);
            if (!m1.Exists(b)) m1.AddItem(b);
            Assert.AreEqual(2, m1.Count());
        }

        [TestMethod()]
        public void FindCheapestTest()
        {
            ItemManager m1 = ItemManager.getInstance();
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
            ItemManager m1 = ItemManager.getInstance();
            m1.ClearList();
            Item a = new Item("Name", "Store", 999, "2017-10-05");
            m1.AddItem(a);
            Assert.AreEqual(1, m1.Count());
        }

        [TestMethod()]
        public void ClearListTest()
        {
            ItemManager m1 = ItemManager.getInstance();
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.AddItem(new Item("Name", "Store", 999, DateTime.Now));
            m1.ClearList();
            Assert.AreEqual(0, m1.Count());
        }

        [TestMethod()]
        public void PersistTest_savesItems()
        {
            ItemManager m1 = ItemManager.getInstance();
            Item testItem = new Item("Name", "Store", 999, DateTime.Now);
            string storageDir = ConfigurationManager.AppSettings["storageDir"];
            m1.AddItem(testItem);

            m1.Persist();

            m1.ClearList();
            DirectoryInfo storageDirInfo = new DirectoryInfo(storageDir);
            Assert.AreEqual(1, storageDirInfo.GetFiles("*.item").Length);
            Assert.AreEqual($"{ testItem.GetHashCode().ToString().PadLeft(10, '0').Substring(0, 10) }.item", storageDirInfo.GetFiles("*.item")[0].Name);
        }

        [TestMethod()]
        public void LoadAllTest_loadsItems()
        {
            ItemManager m1 = ItemManager.getInstance();
            Item testItem = new Item("Name", "Store", 999, DateTime.Now);
            m1.AddItem(testItem);

            m1.Persist();
            m1.ClearList();

            Assert.IsFalse(m1.Exists(testItem));

            m1.LoadAll();
            
            Assert.IsTrue(m1.Exists(testItem));
        }
    }
}