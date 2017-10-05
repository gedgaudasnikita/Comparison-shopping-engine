using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public class StoreParser: IParser<string>
    {
        private int maximumMatchingDistance = 7;

        private string[] supportedStores;
        private int minimumStoreNameLength;
        private int maximumStoreNameLength;

        public StoreParser()
        {
            int.TryParse(ConfigurationManager.AppSettings["maximumMatchingDistance"], out maximumMatchingDistance);
            supportedStores = ConfigurationManager.AppSettings["supportedStores"].Split(',');
            minimumStoreNameLength = supportedStores.Min(storeName => storeName.Length);
            maximumStoreNameLength = supportedStores.Max(storeName => storeName.Length);
        }

        public string Parse(string source)
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

    }
}
