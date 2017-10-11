using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine.Controller
{
    public static class Controller
    {
        /// <summary>
        /// Processes given <see langword="Bitmap"/> into a receipt with its items replaces with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see langword="Bitmap"/> to process</param>
        /// <returns></returns>
        public static Receipt ProcessReceipt(Bitmap source)
        {
            Receipt receipt = Receipt.Convert(source);
            return ProcessReceipt(receipt);
        }

        /// <summary>
        /// Compares given <see cref="Receipt"> items and replaces them with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see cref="Receipt"> to process</param>
        /// <returns></returns>
        public static Receipt ProcessReceipt(Receipt source)
        {
            ItemManager manager = ItemManager.GetInstance();
            List<Item> cheapList = new List<Item>();
            manager.Add(source.Items);
            foreach (Item item in source.Items)
            {
                cheapList.Add(manager.FindCheapest(item));
            }
            source.Items = cheapList;
            return source;
        }
    }
}
