using Comparison_shopping_engine_core_entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// A class representing the ParseableReceipt entity. Adds the parsing functionality needed in the backend
    /// </summary>
    public class ParseableReceipt: Receipt
    {
        //Static fields
        //Mainly parsers, enabling Dependency Injection

        public static IParser<string> StoreParser { private get; set; }
        public static IParser<List<Item>> ItemListParser { private get; set; }
        public static IParser<DateTime> DateParser { private get; set; }

        /// <summary>
        /// Creates a new <see cref="ParseableReceipt"/> instance and fills it with information, parsed from
        /// <paramref name="source"/>, using the supplied parsers, specific to each relative field of
        /// <see cref="Parseable"/>
        /// </summary>
        /// <param name="source">A <see cref="string"/> to parse</param>
        /// <returns>A newly created <see cref="Parseable"/></returns>
        public static ParseableReceipt Parse(string source)
        {
            ParseableReceipt result = new ParseableReceipt();

            // In perspective, null cases should throw a custom exception - this should not be a normal flow
            if (StoreParser != null)
            {
                result.Store = StoreParser.Parse(source);
            }

            if (ItemListParser != null)
            {
                result.Items = ItemListParser.Parse(source);
            }

            if (DateParser != null)
            {
                result.Date = DateParser.Parse(source);
            }

            //Make sure the dates and stores are all the same
            foreach (Item item in result.Items)
            {
                item.Date = result.Date;
                item.Store = result.Store;
            }
          
            return result;
        }
    }
}
