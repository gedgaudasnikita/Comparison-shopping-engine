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
    /// The class that represents the element, responsible for the deletion of the ItemLine
    /// </summary>
    public class ItemDeleteView: TextView
    {
        public ItemDeleteView(Context ctx) : base(ctx)
        {
            SetTextSize(Android.Util.ComplexUnitType.Pt, 7);
            SetTextColor(Android.Graphics.Color.Red);
        }
    }
}