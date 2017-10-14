using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine.Tests
{
    [TestClass()]
    public class ControllerTests
    {
        [TestMethod()]
        public void ProcessReceiptTest()
        {
            ItemManager manager = ItemManager.GetInstance();
            manager.ClearList();

            manager.Add(new Item("Name A", "Store A", 1915, DateTime.Now.Date));
            manager.Add(new Item("Name B", "Store A", 915, DateTime.Now.Date));
            manager.Add(new Item("Name A", "Store B", 2300, DateTime.Now.Date));
            manager.Add(new Item("Name A", "Store C", 1566, DateTime.Now.Date));
            manager.Add(new Item("Name B", "Store A", 2000, DateTime.Now.Date));

            List<Item> list = new List<Item>();
            list.Add(new Item("Name A", "Store D", 2099, DateTime.Now.Date));
            list.Add(new Item("Name B", "Store D", 600, DateTime.Now.Date));
            list.Add(new Item("Name A", "Store D", 1899, DateTime.Now.Date));

            Receipt receipt = new Receipt();
            receipt.Items = list;
            //Controller.ProcessReceipt(receipt, (string Source) => { return; });

            Item item = receipt.Items.ElementAt(0);
            Item item1 = receipt.Items.ElementAt(1);
            Item item2 = receipt.Items.ElementAt(2);

            //Assert.IsTrue(item.Equals(item2) && item1.Equals(new Item("Name B", "Store D", 600, DateTime.Now.Date)));
        }
    }
}