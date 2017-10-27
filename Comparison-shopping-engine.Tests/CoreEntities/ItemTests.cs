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
    public class ItemTests
    {
        [TestMethod()]
        public void ItemTest_constructsWithDateTime()
        {
            Item i = new Item("Name", "Store", 999, DateTime.Now);
            Assert.IsNotNull(i);
        }

        [TestMethod()]
        public void ItemTest_constructsWithDateString()
        {
            Item i = new Item("Name", "Store", 999, "2017-10-10");
            Assert.IsNotNull(i);
        }

        [TestMethod()]
        public void EqualsTest_equalsCorrectly()
        {
            Item a = new Item("Name", "Store", 999, "2017-10-10");
            Item b = new Item("Name", "Store", 999, "2017-10-10");
            Item c = new Item("Other", "Other", 999, "2017-10-10");
            Assert.IsTrue(a.Equals(b) && !a.Equals(c));
        }
    }
}