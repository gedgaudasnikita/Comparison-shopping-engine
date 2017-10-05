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
        //FORMATEXCEPTION ALERT! Fix ASAP
        private static int maximumMatchingDistance = int.Parse(ConfigurationManager.AppSettings["maximumMatchingDistance"]);
        
        private static string[] supportedStores = ConfigurationManager.AppSettings["supportedStores"].Split(',');
        private static int minimumStoreNameLength = supportedStores.Min(storeName => storeName.Length);
        private static int maximumStoreNameLength = supportedStores.Max(storeName => storeName.Length);
        
        //Lines ending with three numbers and tax groups A or M1 are what we consider items
        private static string itemRegex = @"(.+)(\d.*\d.*\d.*(A|M1))";

        public static string ParseStore(string source)
        {
            string parsedStore = "";

            //Larger distances give virtually random results, better consider the store unparseable
            int minScore = maximumMatchingDistance;

            //We iterate through the whole source, because there is no guaranteed placement of the store name
            for (int index = 0; index < source.Length; index++)
            {
                //Matching all possible substrings is inefficient, limit that to relevants only
                for (int length = minimumStoreNameLength; length < maximumStoreNameLength && length + index < source.Length; length++)
                {
                    //Calculate a matching score for each store name; smallest wins
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
            string itemPriceClean = matchedItem.Groups[2].Value.RemoveNonDigits();
            int.TryParse(itemPriceClean, out itemPrice);

            return parsedItem;
        }

    */
    }
}
