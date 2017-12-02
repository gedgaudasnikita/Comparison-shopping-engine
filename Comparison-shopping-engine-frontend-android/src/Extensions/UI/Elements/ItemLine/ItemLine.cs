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
    public class ItemLine: IEquatable<ItemLine>
    {
        public ItemNameView Name { get; set; }
        public ItemPriceView Price { get; set; }

        public bool Equals(ItemLine other)
        {
            return (Name.Id == other.Name.Id) && (Price.Id == other.Price.Id);
        }

        public bool Validate()
        {
            return Price.StateManager.State != ItemInfoStates.INFO_WRONG;
        }
    }
}