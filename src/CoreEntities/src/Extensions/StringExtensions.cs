using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_core_entities
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

    }
}
