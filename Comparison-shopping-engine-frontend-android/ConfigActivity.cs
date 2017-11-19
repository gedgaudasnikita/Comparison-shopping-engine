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
    [Activity(Label = "CoShE Config")]
    public class ConfigActivity : Activity
    {
        private Spinner spinner;
        private TextView requestText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Config);

            spinner = FindViewById<Spinner>(Resource.Id.configSpinner);
            requestText = FindViewById<TextView>(Resource.Id.configTextView);

            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Themes, Android.Resource.Layout.SimpleSpinnerDropDownItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);

            Localise();
            
        }

        protected void Localise()
        {
            requestText.Text = AppResources.ConfigRequest;
        }

        private void SpinnerItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string theme = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

            switch (theme)
            {
                case "Dark":
                    AppData.theme = Android.Resource.Style.ThemeMaterial;
                    Finish();
                    break;
                case "Light":
                    AppData.theme = Android.Resource.Style.ThemeMaterialLight;
                    Finish();
                    break;
                default:
                    break;
            }
        }
    }
}