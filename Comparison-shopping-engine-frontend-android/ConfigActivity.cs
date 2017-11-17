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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Config);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.configSpinner);

            SetUpSpinner(spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(SpinnerItemSelected);
            
        }

        private void SetUpSpinner(Spinner spinner)
        {
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.Themes, Android.Resource.Layout.SimpleSpinnerDropDownItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;
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