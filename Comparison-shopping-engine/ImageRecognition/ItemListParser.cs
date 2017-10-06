using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    class ItemListParser: IParser<List<Item>>
    {
        //Lines ending with three numbers and tax groups A or M1 are what we consider items
        private string itemRegex = @"(.+)(\d.*\d.*\d.*)(?:A|M1)";

        public List<Item> Parse(string source)
        {
            List<Item> parsedItems = new List<Item>();

            var itemMatch = Regex.Match(source, itemRegex);

            while (itemMatch.Success)
            {
                parsedItems.Add(ParseItem(itemMatch));
                itemMatch = itemMatch.NextMatch();
            }

            return parsedItems;
        }

        private Item ParseItem(Match matchedItem)
        {
            Item parsedItem = new Item();

            string itemName = matchedItem.Groups[1].Value;

            int itemPrice = 0;
            string itemPriceClean = matchedItem.Groups[2].Value.RemoveNonDigits();
            int.TryParse(itemPriceClean, out itemPrice);

            return parsedItem;
        }
    }
}
