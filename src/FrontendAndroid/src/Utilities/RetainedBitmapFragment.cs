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
using Android.Graphics;

namespace Comparison_shopping_engine_frontend_android.src.Utilities
{
    /// <summary>
    /// A class for a retained picture after configuration change
    /// </summary>
    class RetainedBitmapFragment : Fragment
    {
        public Bitmap Picture
        { get; set; }

        //this method is only called once for this fragment
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //retain this fragment
            this.RetainInstance = true;
        }  
    }
}