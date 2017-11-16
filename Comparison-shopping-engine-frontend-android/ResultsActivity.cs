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
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "CoShE Results")]
    public class ResultsActivity : Activity
    {
        BackendInterface backendInterface = new BackendInterface();

        LinearLayout resultsLinearLayout;
        Button resultsNewItemButton;
        LinearLayout itemsLinearLayout;

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

            // Set up itemsLinearLayout
            itemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.itemsLinearLayout);

            // Get passed receiptText if it's passed, divide it into items and add them as separate TextViews in main LinearLayout
            ProcessReceipt();

            // Add Button and itemsLinearLayout to resultsLinearLayout
            resultsLinearLayout.AddView(resultsNewItemButton);
            resultsLinearLayout.AddView(itemsLinearLayout);

            resultsNewItemButton.Click += OnResultsNewItemButtonClick;
        }

        private void OnResultsNewItemButtonClick(object sender, EventArgs e)
        {
            LinearLayout item = NewItem();
            itemsLinearLayout.AddView(item);
        }

        private async void ProcessReceipt()
        {
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? ("NoReceipt");

            if (!receiptText.Equals("NoReceipt"))
            {
                TextView receipt = new TextView(this);
                receipt.Text = receiptText;
                resultsLinearLayout.AddView(receipt);
            }

            BackendInterface backendIterface = new BackendInterface();
            Receipt receiptToProcess = await backendInterface.ProcessImage(receiptText);
            List<Item> itemList = await backendInterface.ProcessReceipt(receiptToProcess);
            
            foreach(var item in itemList)
            {
                this.itemsLinearLayout.AddView(new EditText(this)
                {
                    Text = item.Store,
                    Clickable = true,
                    Focusable = true,
                    FocusableInTouchMode = true
                });
                this.itemsLinearLayout.AddView(new EditText(this)
                {
                    Text = item.Date.ToString("yyyy-MM-dd"),
                    Clickable = true,
                    Focusable = true,
                    FocusableInTouchMode = true
                });
                this.itemsLinearLayout.AddView(new EditText(this)
                {
                    Text = item.Name,
                    Clickable = true,
                    Focusable = true,
                    FocusableInTouchMode = true
                });
                this.itemsLinearLayout.AddView(new EditText(this)
                {
                    Text = item.Price.ToString(),
                    Clickable = true,
                    Focusable = true,
                    FocusableInTouchMode = true
                });
            }
        }

        private LinearLayout NewItem()
        {
            LinearLayout itemLayout = new LinearLayout(this);
            itemLayout.LayoutParameters = new LinearLayout.LayoutParams(width: ViewGroup.LayoutParams.MatchParent, height: ViewGroup.LayoutParams.WrapContent);
            EditText itemName = new EditText(this);
            EditText itemStore = new EditText(this);
            EditText itemDate = new EditText(this);
            EditText itemPrice = new EditText(this);

            itemLayout.AddView(itemName);
            itemLayout.AddView(itemStore);
            itemLayout.AddView(itemDate);
            itemLayout.AddView(itemPrice);

            return itemLayout;
        }
    }
}