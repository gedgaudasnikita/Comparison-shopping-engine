using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public delegate void StringResultCallback(string result);
    public static class Controller
    {
        /// <summary>
        /// Processes given <see langword="Bitmap"/> into a receipt with its items replaces with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see langword="Bitmap"/> to process</param>
        /// <returns></returns>
        public static void ProcessReceipt(Bitmap source, StringResultCallback callback)
        {
            Receipt receipt = Receipt.Convert(source);
            ProcessReceipt(receipt, callback);
        }

        /// <summary>
        /// Compares given <see cref="Receipt"> items and replaces them with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see cref="Receipt"> to process</param>
        /// <returns></returns>
        public static void ProcessReceipt(Receipt source, StringResultCallback callback)
        {
            String result = "Source: ";

            foreach (Item item in source.Items)
            {
                result += "\n" + item.ToString();
            }


            ItemManager manager = ItemManager.GetInstance();
            List<Item> cheapList = new List<Item>();
            manager.Add(source.Items);
            foreach (Item item in source.Items)
            {
                cheapList.Add(manager.FindCheapest(item));
            }

            result += "\nCheaper: ";

            foreach (Item item in cheapList)
            {
                result += "\n" + item.ToString();
            }

            callback(result);
        }
    }
}
