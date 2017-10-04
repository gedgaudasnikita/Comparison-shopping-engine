using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public static class Parser
    {
        public const int MIN_STORE_LENGTH = 2;
        public const int MAX_STORE_LENGTH = 7;
        private static string[] supportedStores = ConfigurationManager.AppSettings["supportedStores"].Split(',');
        private static string itemRegex = @"(.+)(\d.*\d.*\d.*A)";

        public static string ParseStore(string source)
        {
            string parsedStore = "";
            int minScore = int.MaxValue;

            for (int index = 0; index < source.Length; index++)
            {
                for (int length = MIN_STORE_LENGTH; length < MAX_STORE_LENGTH && length + index < source.Length; length++)
                {
                    foreach(string store in supportedStores)
                    {
                        int currentScore = store.GetDistance(source.Substring(index, length));

                        if (currentScore < minScore)
                        {
                            minScore = currentScore;
                            parsedStore = store;
                        }
                    }
                }
            }
            source.Substring(0, 0);

            Console.WriteLine(minScore);
            return parsedStore;
        }

        /*
        public static List<Item> ParseItems(string source)
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

        private static Item ParseItem(Match matchedItem)
        {
            Item parsedItem = new Item();

            string itemName = matchedItem.Groups[1].Value;

            int itemPrice = 0;
            string itemPriceClean = matchedItem.Groups[2].Value.RemoveDigits();
            int.TryParse(itemPriceClean, out itemPrice);

            return parsedItem;
        }

    */
    }
}
