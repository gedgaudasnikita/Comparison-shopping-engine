using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_core_entities.Tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void ItemTest_constructsWithDateTime()
        {
            Item i = new Item("Name", "Store", 999, DateTime.Now);
            Assert.IsNotNull(i);
        }

        [Test]
        public void ItemTest_constructsWithDateString()
        {
            Item i = new Item("Name", "Store", 999, "2017-10-10");
            Assert.IsNotNull(i);
        }

        [Test]
        public void EqualsTest_equalsCorrectly()
        {
            Item a = new Item("Name", "Store", 999, "2017-10-10");
            Item b = new Item("Name", "Store", 999, "2017-10-10");
            Item c = new Item("Other", "Other", 999, "2017-10-10");
            Assert.IsTrue(a.Equals(b) && !a.Equals(c));
        }
    }
}