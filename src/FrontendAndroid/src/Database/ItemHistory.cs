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
using SQLite;

namespace Comparison_shopping_engine_frontend_android
{
    public class ItemHistory
    {
        [PrimaryKey]
        public string ItemName { get; set; }
        public int Count { get; set; }
    }
}