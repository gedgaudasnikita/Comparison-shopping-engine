using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    /// <summary>
    /// A class representing the Receipt entity. Mainly used for encapsualting the results of image recognition.
    /// </summary>
    class Receipt
    {
        //Static fields
        //Mainly parsers, enabling Dependency Injection
        private static IParser<string> storeParser;
        private static IParser<List<Item>> itemListParser;
        private static IParser<DateTime> dateParser;

        public static IParser<string> StoreParser { private get; set; }
        public static IParser<List<Item>> ItemListParser { private get; set; }
        public static IParser<DateTime> dateParser { private get; set; }

        //Instance fields
        //User provided data, encapsulated in one entity
        private string store;
        private List<Item> items;
        private DateTime date;

        public string Store { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Date { get; set; }

        /// <summary>
        /// Converts a given <see langword="Bitmap"/> to a Receipt entity with the parsed information
        /// </summary>
        /// <param name="source">A <see langword="Bitmap"/> to convert from</param>
        /// <returns>A converted <see cref="Receipt"/></returns>
        public static Receipt Convert(Bitmap source)
        {
            var text = OCRWrapper.ConvertToText(source);
            return Parse(text);
        }

        /// <summary>
        /// Creates a new <see cref="Receipt"/> instance and fills it with information, parsed from
        /// <paramref name="source"/>, using the supplied parsers, specific to each relative field of
        /// <see cref="Receipt"/>
        /// </summary>
        /// <param name="source">A <see langword="string"/> to parse</param>
        /// <returns>A newly created <see cref="Receipt"/></returns>
        public static Receipt Parse(string source)
        {
            Receipt result = new Receipt();

            // In perspective, null cases should throw a custom exception - this should not be a normal flow
            if (storeParser != null)
            {
                result.Store = storeParser.Parse(source);
            }

            if (itemListParser != null)
            {
                result.Items = itemListParser.Parse(source);
            }

            if (dateParser != null)
            {
                result.Date = dateParser.Parse(source);
            }

            return result;
        }

    }
}
