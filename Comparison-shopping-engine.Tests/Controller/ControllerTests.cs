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
        public void ProcessReceiptTest_processesReceipt()
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

            Receipt resultReceipt;
            List<Item> cheaperItems = list;
            Controller.ProcessReceipt(receipt, (resultReceiptReceived, cheaperItemsReceived) => {
                resultReceipt = resultReceiptReceived;
                cheaperItems = cheaperItemsReceived;
            });

            Item item = cheaperItems.ElementAt(0);
            Item item1 = cheaperItems.ElementAt(1);
            Item item2 = cheaperItems.ElementAt(2);

            Assert.IsTrue(item.Equals(item2) && item1.Equals(new Item("Name B", "Store D", 600, DateTime.Now.Date)));
        }
    }
}