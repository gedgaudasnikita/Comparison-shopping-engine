using Comparison_shopping_engine_core_entities;
using Comparison_shopping_engine_web_protocol;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class is used for normalisation of <see cref="string"/> names and storing the fixed names.
    /// Intended for OCR result correction and autocomplete suggestion mechanisms.
    /// </summary>
    public static class NormalizationEngine
    {
        private static List<string> names = new List<string>();

        /// <summary>
        /// Add a new fixed name into the name storage. Should only be used with confirmed names,
        /// for example, the ones manually entered by User. 
        /// </summary>
        /// <param name="name">The <see cref="string"/> to be added</param>
        public static void AddName(string name)
        {
            names.Add(name);
        }

        /// <summary>
        /// Check if a given name is stored
        /// </summary>
        /// <param name="name">The <see cref="string"/> to be checked</param>
        /// <returns>A <see cref="bool"/> indicating whether the name is stored or not</returns>
        public static bool Exists(string name)
        {
            return names.Contains(name);
        }

        /// <summary>
        /// Clear the internal list
        /// </summary>
        public static void ClearList()
        {
            names.Clear();
        }

        /// <summary>
        /// Get a closest string match from the name storage. Intended to use for initial data normalisation.
        /// </summary>
        /// <param name="name">A <see cref="string"/> to be matched</param>
        /// <returns>A <see cref="string"/> with the closest match</returns>
        public static string GetClosest(string name)
        {
            if (names.Count == 0)
            {
                return "";
            } else
            {
                //Ensures we only iterate through the collection once.
                return names.Aggregate((name1, name2) => name1.GetDistance(name) > name2.GetDistance(name) ? name2 : name1);
            }
        }

        /// <summary>
        /// Get a list of the closest matches for a particular name. Intended to use for generating suggestions.
        /// In case where <paramref name="limit"/> is 1, it is recommended to use <see cref="getClosest(string)"/>
        /// for more efficient implementation.
        /// </summary>
        /// <param name="name">A <see cref="string"/> to be matched</param>
        /// <param name="limit">An <see cref="int"/>, indicating the preferred amount of matches to return</param>
        /// <returns>A <see cref="List{string}"/> of the closest matches</returns>
        public static List<string> GetClosestList(string name, int limit)
        {
            return new List<string>(names.OrderBy(savedName => savedName.GetDistance(name)).Take(limit));
        }

        /// <summary>
        /// A method deserializing all of the names currently saved
        /// from a User configured location.
        /// Deletes all the old names before reading.
        /// </summary>
        public static void LoadAll()
        {
            ClearList();

            using (ItemsContext db = new ItemsContext())
            {
                var allNames = from item in db.Items select item.Name;
                names = allNames.ToList();
            }
        }
    }
}
