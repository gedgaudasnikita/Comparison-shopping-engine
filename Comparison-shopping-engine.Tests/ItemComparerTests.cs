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
    public class ItemComparerTests
    {
        [TestMethod()]
        public void EqualNameTest()
        {
            Item a = new Item("Name", "StoreA", 999, "2017-10-05");
            Item b = new Item("Name", "StoreB", 1999, "2017-10-06");
            Assert.IsTrue(ItemComparer.EqualName(a, b));
        }

        [TestMethod()]
        public void EqualStoreTest()
        {
            Item a = new Item("NameA", "Store", 999, "2017-10-05");
            Item b = new Item("NameB", "Store", 1999, "2017-10-06");
            Assert.IsTrue(ItemComparer.EqualStore(a, b));
        }

        [TestMethod()]
        public void IsEqualTest()
        {
            Item a = new Item("Name", "Store", 999, "2017-10-05");
            Item b = new Item("Name", "Store", 999, "2017-10-05");
            Assert.IsTrue(ItemComparer.IsEqual(a, b));
        }

        [TestMethod()]
        public void IsSameItemOlderTest()
        {
            Item a = new Item("Name", "Store", 1999, "2017-10-01");
            Item b = new Item("Name", "Store", 999, "2017-10-05");
            Assert.IsTrue(ItemComparer.IsSameItemOlder(a, b));
        }

        [TestMethod()]
        public void IsSameItemNewerTest()
        {
            Item a = new Item("Name", "Store", 1999, "2017-10-05");
            Item b = new Item("Name", "Store", 999, "2017-10-01");
            Assert.IsTrue(ItemComparer.IsSameItemNewer(a, b));
        }

        [TestMethod()]
        public void IsSameNameCheaperTest()
        {
            Item a = new Item("Name", "StoreA", 599, "2017-10-05");
            Item b = new Item("Name", "StoreB", 999, "2017-10-01");
            Assert.IsTrue(ItemComparer.IsSameNameCheaper(a, b));
        }

        [TestMethod()]
        public void IsCheaperTest()
        {
            Item a = new Item("NameA", "StoreA", 599, "2017-10-06");
            Item b = new Item("NameB", "StoreB", 700, "2014-12-12");
            Assert.IsTrue(ItemComparer.IsCheaper(a, b));
        }
    }
}