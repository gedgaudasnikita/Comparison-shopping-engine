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
        public ImageView pictureImageView;

        //this method is only called once for this fragment
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //retain this fragment
            this.RetainInstance = true;
            inflater.Inflate(Resource.Layout., container, false);

            pictureImageView = inflater.
        }  
    }
}