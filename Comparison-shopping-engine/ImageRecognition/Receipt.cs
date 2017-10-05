using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    class Receipt
    {
        private string store;
        private List<Item> items;

        private static IParser<string> storeParser;
        private static IParser<List<Item>> itemListParser;

        public string Store { get; set; }
        public List<Item> Items { get; set; }

        public static IParser<string> StoreParser { private get; set; }
        public static IParser<List<Item>> ItemListParser { private get; set; }

        public static Receipt Parse(Bitmap source)
        {
            var text = OCRWrapper.ConvertToText(source);
            return Parse(text);
        }

        public static Receipt Parse(string source)
        {
            Receipt result = new Receipt();

            if (storeParser != null)
            {
                result.Store = storeParser.Parse(source);
            }

            if (itemListParser != null)
            {
                result.Items = itemListParser.Parse(source);
            }

            return result;
        }

    }
}
