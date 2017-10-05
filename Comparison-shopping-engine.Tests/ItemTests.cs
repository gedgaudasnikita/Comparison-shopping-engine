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
        public void ItemTest()
        {
            Item i = new Item("Name", "Store", 9.99, DateTime.Now);
            Assert.IsNotNull(i);
        }

        [TestMethod()]
        public void ItemTest1()
        {
            Item i = new Item("Name", "Store", 9.99, "2017-10-10");
            Assert.IsNotNull(i);
        }

        [TestMethod()]
        public void PrintTest()
        {
            Assert.IsTrue(true);
        }
    }
}