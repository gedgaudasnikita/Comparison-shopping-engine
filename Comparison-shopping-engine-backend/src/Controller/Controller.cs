using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{ 
    /// <summary>
    /// This class connects the UX components with the core entities.
    /// Essentially this class is responsible for the application behaviour
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Processes given <see cref="Bitmap"/> into a <see cref="Receipt"> with normalised names
        /// </summary>
        /// <param name="source">A <see cref="Bitmap"/> to process</param>
        /// <returns>A parsed <see cref="Receipt"></returns>
        public static Receipt ProcessImage(Bitmap source)
        {
            Receipt receipt = Receipt.Convert(source);

            var normalizer = NormalizationEngine.GetInstance();
            receipt.Items.ForEach(item => item.Name = normalizer.GetClosest(item.Name));

            return receipt;
        }

        /// <summary>
        /// Compares given <see cref="Receipt"> items and returns the with cheapest alternatives found
        /// </summary>
        /// <param name="source">A <see cref="Receipt"> to process</param>
        /// <returns>A <see cref="List{Item}"> representing the cheapest items found</returns>
        public static List<Item> ProcessReceipt(Receipt source)
        {
            ItemManager manager = ItemManager.GetInstance();
            manager.Add(source.Items);

            manager.Persist();

            List<Item> cheapList = source.Items.Select(item => manager.FindCheapest(item)).ToList();

            return cheapList;
        }
    }
}
