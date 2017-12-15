using Comparison_shopping_engine_core_entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [Test]
        public void ProcessImageTest_parsesReceipt()
        {
            ParseableReceipt.DateParser = new DateParser();
            ParseableReceipt.ItemListParser = new ItemListParser();
            ParseableReceipt.StoreParser = new StoreParser();
            var image = "maxima\nfavorit 2.33A";

            var result = Controller.ProcessImage(image);

            Assert.IsTrue(new Receipt()
            {
                Store = "MAXIMA",
                Date = DateTime.Now.Date,
                Items = new List<Item>() { new Item("", "MAXIMA", 233, DateTime.Now.Date) }
            }.Equals(result));
        }
    }
}