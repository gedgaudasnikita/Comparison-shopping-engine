using Android.App;
using Android.Widget;
using Android.OS;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "Comparison_shopping_engine_frontend_android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

