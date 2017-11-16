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
        LinearLayout resultsLinearLayout;
        Button resultsNewItemButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "Results" layout resource
            SetContentView(Resource.Layout.Results);

            // Set up layout and create list of items
            resultsLinearLayout = FindViewById<LinearLayout>(Resource.Id.resultsLinearLayout);

            // Button for adding new items to the list
            resultsNewItemButton = new Button(this);
            resultsNewItemButton.Text = "New Item";

            // Get passed receiptText if it's passed, should be separate function, for testing only
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? ("NoReceipt");

            if (!receiptText.Equals("NoReceipt"))
            {
                TextView receipt = new TextView(this);
                receipt.Text = receiptText;
                resultsLinearLayout.AddView(receipt);
            }

            //Add Items to LinearLayout
            resultsLinearLayout.AddView(resultsNewItemButton);

        }
    }
}