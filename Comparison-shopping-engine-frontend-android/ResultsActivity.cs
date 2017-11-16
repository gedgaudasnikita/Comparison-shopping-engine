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
    [Activity(Label = "CoShE Results")]
    public class ResultsActivity : Activity
    {
        BackendInterface backendInterface = new BackendInterface();

        LinearLayout resultsLinearLayout;
        Button resultsNewItemButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);
            // Set our view from the "Results" layout resource
            SetContentView(Resource.Layout.Results);

            // Set up layout and create list of items
            resultsLinearLayout = FindViewById<LinearLayout>(Resource.Id.resultsLinearLayout);

            // Button for adding new items to the list
            resultsNewItemButton = new Button(this);
            resultsNewItemButton.Text = "New Item";

            // Get passed receiptText if it's passed, divide it into items and add them as separate TextViews in main LinearLayout
            ProcessReceipt();

            // Add Button to LinearLayout
            resultsLinearLayout.AddView(resultsNewItemButton);

        }

        private void ProcessReceipt ()
        {
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? ("NoReceipt");

            if (!receiptText.Equals("NoReceipt"))
            {
                TextView receipt = new TextView(this);
                receipt.Text = receiptText;
                resultsLinearLayout.AddView(receipt);
            }
        }
    }
}