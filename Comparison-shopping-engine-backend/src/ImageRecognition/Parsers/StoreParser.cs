using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class describes the behaviour of parsing a storeName from plain text.
    /// Intended for usage from <see cref="Receipt"> class
    /// </summary>
    public class StoreParser: IParser<string>
    {
        private string[] supportedStores;
        private int minimumStoreNameLength;
        private int maximumStoreNameLength;
    
        /// <summary>
        /// Constructor method. Initialises the internal variables with data from App.config
        /// </summary>
        public StoreParser()
        {
            supportedStores = ConfigurationManager.AppSettings["supportedStores"].Split(',');
            minimumStoreNameLength = supportedStores.Min(storeName => storeName.Length);
            maximumStoreNameLength = supportedStores.Max(storeName => storeName.Length);
        }

        /// <summary>
        /// Parses the given <see cref="string"/> value and returns the storeName
        /// </summary>
        /// <param name="source">The <see cref="string"/> to be parsed.</param>
        /// <returns>
        /// Returns a <see cref="string"/>, either containing the parsed storeName
        /// or, in case of an unparseable <paramref name="source"/>, empty.
        /// </returns>
        public string Parse(string source)
        {
            string parsedStore = "";

            //Larger distances will always parse the shortest shop name, better consider the store unparseable
            int minScore = minimumStoreNameLength;

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
