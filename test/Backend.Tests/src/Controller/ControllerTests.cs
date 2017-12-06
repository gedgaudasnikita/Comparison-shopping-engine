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

            Console.WriteLine(result.Store);
            Console.WriteLine(result.Date);
            Console.WriteLine(result.Items.Count);
            Console.WriteLine(result.Items[0].ToString());

            Assert.IsTrue(new Receipt()
            {
                Store = "MAXIMA",
                Date = DateTime.Now.Date,
                Items = new List<Item>() { new Item("", "MAXIMA", 233, DateTime.Now.Date) }
            }.Equals(result));
        }

        [Test]
        public void ProcessReceiptTest_processesReceipt()
        {
            ItemManager manager = ItemManager.GetInstance();
            manager.ClearList();

            manager.Add(new Item("Name A", "Store A", 1915, DateTime.Now.Date));
            manager.Add(new Item("Name B", "Store A", 915, DateTime.Now.Date));
            manager.Add(new Item("Name A", "Store B", 2300, DateTime.Now.Date));
            manager.Add(new Item("Name A", "Store C", 1566, DateTime.Now.Date));
            manager.Add(new Item("Name B", "Store A", 2000, DateTime.Now.Date));

            List<Item> list = new List<Item>
            {
                new Item("Name A", "Store D", 2099, DateTime.Now.Date),
                new Item("Name B", "Store D", 600, DateTime.Now.Date),
                new Item("Name A", "Store D", 1899, DateTime.Now.Date)
            };

            Receipt receipt = new Receipt { Items = list };

            List<Item> cheaperItems = list;
            Controller.SaveReceipt(receipt);
            cheaperItems = Controller.ProcessReceipt(receipt);

            Item item = cheaperItems.ElementAt(0);
            Item item1 = cheaperItems.ElementAt(1);
            Item item2 = cheaperItems.ElementAt(2);

            Assert.IsTrue(item.Equals(item2) && item1.Equals(new Item("Name B", "Store D", 600, DateTime.Now.Date)));
        }
    }
}