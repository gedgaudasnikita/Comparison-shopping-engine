using Comparison_shopping_engine_core_entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class describes the behaviour of parsing a storeName from plain text.
    /// Intended for usage from <see cref="ParseableReceipt"> class.
    /// </summary>
    public class DateParser: IParser<DateTime>
    {
        //Basically searching for 8 digits and whatever (of reasonable amount) rubbish inbetween them
        private string dateRegex = @"(\d\D{0,3}\d\D{0,3}\d\D{0,3}\d\D{0,3}\d\D{0,3}\d\D{0,3}\d\D{0,3}\d)";

        /// <summary>
        /// Parses the given <see cref="string"/> value and returns the date.
        /// </summary>
        /// <param name="source">The <see cref="string"/> to be parsed.</param>
        /// <returns>
        /// Returns a <see cref="DateTime"/> with the information, 
        /// parsed from <paramref name="source"/>, or with the current date, in case
        /// parsing was unsuccessful
        /// </returns>
        public DateTime Parse(string source)
        {
            //If our regex or parsing did not work, assume the default is current date
            DateTime parsedDate = DateTime.Now.Date;

            long minDifference = long.MaxValue;

            var dateMatch = Regex.Match(source, dateRegex);

            //Since we can't do better than to simply look for 9 numbers, there might be 
            //a lot of wrong numbers involved. We have to check throught them all, and find
            //not only the valid, but also the most believable (closest the to current date)
            while (dateMatch.Success)
            {
                DateTime tempDate = new DateTime();
                if (DateTime.TryParseExact(dateMatch.Value.RemoveNonDigits(), 
                    //All lithuanian dates follow this format, ignoring the separators
                    "yyyyMMdd", 
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, 
                    out tempDate)) 
                {
                    long currentDifference = (parsedDate - DateTime.Now.Date).Ticks;

                    if (currentDifference < minDifference)
                    {
                        minDifference = currentDifference;
                        parsedDate = tempDate;
                    }
                }

                dateMatch = dateMatch.NextMatch();
            }

            //We really dont need the Time part in our internal calculations
            return parsedDate.Date;
        }
    }
}
