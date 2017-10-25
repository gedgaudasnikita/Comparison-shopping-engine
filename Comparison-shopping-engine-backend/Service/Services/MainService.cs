using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Comparison_shopping_engine_backend
{
    public class MainService : IService
    {
        static MainService()
        {
            Receipt.ItemListParser = new ItemListParser();
            Receipt.DateParser = new DateParser();
            Receipt.StoreParser = new StoreParser();
            ItemManager.GetInstance().LoadAll();
            NormalizationEngine.GetInstance().LoadAll();
        }

        /// <summary>
        /// Processes given <see langword="Bitmap"/> into a <see cref="Receipt"/> and returns it for moderation
        /// </summary>
        /// <param name="source">A <see langword="Bitmap"/> to process</param>
        /// <returns>A <see cref="Receipt"/> with the parsed information</returns>
        public Receipt ProcessImage(Bitmap source)
        {
            Receipt receipt = Receipt.Convert(source);

            var normalizer = NormalizationEngine.GetInstance();
            receipt.Items.ForEach(item => item.Name = normalizer.GetClosest(item.Name));

            return receipt;
        }

        /// <summary>
        /// Compares the <see cref="Item"/> objects in the given <see cref="Receipt"> and returns 
        /// a <see cref="List{Item}"/> with the cheaper stored records
        /// </summary>
        /// <param name="source">A <see cref="Receipt"> to process</param>
        /// <returns>A <see cref="List{Item}"/> of the cheapest items</returns>
        public List<Item> ProcessReceipt(Receipt source)
        {
            ItemManager manager = ItemManager.GetInstance();
            manager.Add(source.Items);

            manager.Persist();

            List<Item> cheapList = source.Items.Select(item => manager.FindCheapest(item)).ToList();

            return cheapList;
        }

        public Receipt Test()
        {
            return new Receipt();
        }
    }
}
