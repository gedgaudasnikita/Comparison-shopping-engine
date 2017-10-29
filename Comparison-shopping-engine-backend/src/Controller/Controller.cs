using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    public delegate void ComparisonResultCallback(Receipt parsed, List<Item> itemLists);

    /// <summary>
    /// This class connects the UX components with the core entities.
    /// Essentially this class is responsible for the application behaviour
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Processes given <see cref="Bitmap"/> into a receipt with its items replaces with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see cref="Bitmap"/> to process</param>
        /// <returns></returns>
        public static void ProcessReceipt(Bitmap source, ComparisonResultCallback callback)
        {
            Receipt receipt = Receipt.Convert(source);

            var normalizer = NormalizationEngine.GetInstance();
            receipt.Items.ForEach(item => item.Name = normalizer.GetClosest(item.Name));

            //TODO: Add request for user normalization through the callback

            ProcessReceipt(receipt, callback);
        }

        /// <summary>
        /// Compares given <see cref="Receipt"> items and replaces them with cheapest items in ItemManagers list
        /// </summary>
        /// <param name="source">A <see cref="Receipt"> to process</param>
        /// <returns></returns>
        public static void ProcessReceipt(Receipt source, ComparisonResultCallback callback)
        {
            ItemManager manager = ItemManager.GetInstance();
            manager.Add(source.Items);

            //TODO: Add normalisation and suggestions

            manager.Persist();

            List<Item> cheapList = source.Items.Select(item => manager.FindCheapest(item)).ToList();

            callback(source, cheapList);
        }
    }
}
