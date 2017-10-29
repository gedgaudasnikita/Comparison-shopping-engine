using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// Interface to be implemented by all the <see cref="Receipt"> field parsers.
    /// The generic type T is the type of the respective <see cref="Receipt"> field.
    /// </summary>
    public interface IParser<T>
    {
        /// <summary>
        /// Parses the given <see cref="string"/> value and returns the <see cref="Receipt"> field
        /// </summary>
        /// <param name="source">The <see cref="string"/> to be parsed.</param>
        /// <returns>
        /// Returns a value of type T, containing a parsed value, implementation specific
        /// </returns>
        T Parse(string source);
    }
}
