using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestClass()]
    public class ReceiptTests
    {
        private class ItemListParserMock : IParser<List<Item>>
        {
            public List<Item> Parse(string source)
            {
                List<Item> result = new List<Item>();
                result.Add(new Item("itemName", 100));
                return result;
            }
        }

        private class StoreParserMock : IParser<string>
        {
            public string Parse(string source)
            {
                return "storeName";
            }
        }

        private class DateParserMock : IParser<DateTime>
        {
            public DateTime Parse(string source)
            {
                return new DateTime(1, 1, 1);
            }
        }


        [TestMethod()]
        public void ConvertTest_convertsFromImage()
        {
            Receipt.ItemListParser = new ItemListParserMock();
            Receipt.StoreParser = new StoreParserMock();
            Receipt.DateParser = new DateParserMock();
            var img = new Bitmap("./testdata/receipt.jpg");

            Receipt receipt = Receipt.Convert(img);

            Assert.AreEqual(new DateTime(1, 1, 1), receipt.Date);
            Assert.AreEqual("storeName", receipt.Store);
            Assert.AreEqual(1, receipt.Items.Count);
            Assert.AreEqual(100, receipt.Items[0].Price);
            Assert.AreEqual("itemName", receipt.Items[0].Name);
            Assert.AreEqual(new DateTime(1, 1, 1), receipt.Items[0].Date);
            Assert.AreEqual("storeName", receipt.Items[0].Store);
        }

        [TestMethod()]
        public void ParseTest_parsesFromString()
        {
            Receipt.ItemListParser = new ItemListParserMock();
            Receipt.StoreParser = new StoreParserMock();
            Receipt.DateParser = new DateParserMock();

            Receipt receipt = Receipt.Parse("");

            Assert.AreEqual(new DateTime(1, 1, 1), receipt.Date);
            Assert.AreEqual("storeName", receipt.Store);
            Assert.AreEqual(1, receipt.Items.Count);
            Assert.AreEqual(100, receipt.Items[0].Price);
            Assert.AreEqual("itemName", receipt.Items[0].Name);
            Assert.AreEqual(new DateTime(1, 1, 1), receipt.Items[0].Date);
            Assert.AreEqual("storeName", receipt.Items[0].Store);
        }
    }
}