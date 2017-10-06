using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    /// <summary>
    /// This class describes the behaviour of parsing an date from plain text.
    /// Intended for usage from <see cref="Receipt"> class
    /// </summary>
    class DateParser: IParser<DateTime>
    {
        //Lines ending with three numbers and tax groups A or M1 are what we consider items
        private string dateRegex = @"";

        /// <summary>
        /// Parses the given <see langword="string"/> value and returns the date.
        /// </summary>
        /// <param name="source">The <see langword="string"/> to be parsed.</param>
        /// <returns>
        /// Returns a <see langword="DateTime"/> with the information, 
        /// parsed from <paramref name="source"/>, or with the current date, in case
        /// parsing was unsuccessful
        /// </returns>
        public DateTime Parse(string source)
        {
            DateTime parsedDate = new DateTime();

            var itemMatch = Regex.Match(source, dateRegex);

            //If our regex or parsing did not work, assume the default is current date
            if (!(itemMatch.Success && DateTime.TryParse(itemMatch.Value, out parsedDate)))
            {
                    parsedDate = DateTime.Now;
            }

            //We really dont need the Time part in our internal calculations
            return parsedDate.Date;
        }
    }
}
