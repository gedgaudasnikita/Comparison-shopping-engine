using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// The aggregation class used to represent the UI model of an ItemLine.
    /// Created for the convenience of use in defining the <see cref="ResultsActivity"/> behaviour.
    /// </summary>
    public class ItemLine: IEquatable<ItemLine>
    {
        public ItemNameView Name { get; set; }
        public ItemPriceView Price { get; set; }

        public bool Equals(ItemLine other)
        {
            return (Name.Id == other.Name.Id) && (Price.Id == other.Price.Id);
        }

        /// <summary>
        /// The function to check whether the information for the specific Item is ready to be sent.
        /// Relies on the internal <see cref="StateColorManager"/>s of the elements.
        /// </summary>
        /// <returns>Whether the ItemLine is ready for sending or not</returns>
        public bool Validate()
        {
            return Name.StateManager.State != ItemInfoStates.INFO_WRONG && Price.StateManager.State != ItemInfoStates.INFO_WRONG;
        }
    }
}