using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public static class StringExtension
    {
        public static string RemoveDigits(this string str)
        {
            return new String(str.Where(Char.IsDigit).ToArray());
        }

        public static int GetDistance(this string str, string comparison)
        {
            //Calculating Damerau-Levenshtein distance
            //Credit: https://gist.github.com/wickedshimmy/449595/cb33c2d0369551d1aa5b6ff5e6a802e21ba4ad5c
            int len_orig = str.Length;
            int len_diff = comparison.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = comparison[j - 1] == str[i - 1] ? 0 : 1;
                    var vals = new int[] {
                matrix[i - 1, j] + 1,
                matrix[i, j - 1] + 1,
                matrix[i - 1, j - 1] + cost
            };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && str[i - 1] == comparison[j - 2] && str[i - 2] == comparison[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }
    }
}
