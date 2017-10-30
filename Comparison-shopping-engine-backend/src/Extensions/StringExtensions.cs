using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// The class, containing all the custom extension methods for the primitive <see cref="string"/> type
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Removes all the non-digits from the given string. 
        /// Used in image recognition functionality, more specifically in normalisation processes
        /// </summary>
        /// <param name="str">The <see cref="string"/> for the method to be called on.</param>
        /// <returns>A <see cref="string"/> with digits only</returns>
        public static string RemoveNonDigits(this string str)
        {
            return new String(str.Where(Char.IsDigit).ToArray());
        }

        /// <summary>
        /// Calculates a Damerau-Levenshtein distance between the two given strings.
        /// Used in image recognition functionality, more specifically in normalisation processes
        /// </summary>
        /// <param name="str">The <see cref="string"/> for the method to be called on.</param>
        /// <param name="comparison">The <see cref="string"/> to get distance to (from <paramref name="str"/>).</param>
        /// <returns>The <see cref="int "/> with the Damerau-Levenshtein distance for the two strings</returns>
        public static int GetDistance(this string str, string comparison)
        {
            var first = str.ToLower();
            var second = comparison.ToLower();
            //Credit: https://gist.github.com/wickedshimmy/449595/cb33c2d0369551d1aa5b6ff5e6a802e21ba4ad5c
            int len_orig = first.Length;
            int len_diff = second.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = second[j - 1] == first[i - 1] ? 0 : 1;
                    var vals = new int[] {
                matrix[i - 1, j] + 1,
                matrix[i, j - 1] + 1,
                matrix[i - 1, j - 1] + cost
            };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && first[i - 1] == second[j - 2] && first[i - 2] == second[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }
    }
}
