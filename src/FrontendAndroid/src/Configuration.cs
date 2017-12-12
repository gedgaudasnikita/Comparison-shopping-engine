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
    /// Xamarin provides no sane mechanism of providing configuration to the mobile app except via compiled code.
    /// 🙂🔫
    /// </summary>
    public static class Configuration
    {
        public const string backendUrl = "http://10.0.2.2:4444";
        public const string dbPath = "db.db3";
        public const int topItems = 20;
    }
}