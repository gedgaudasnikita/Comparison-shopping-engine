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
        Button resultsNewItemButton, resultsSubmitButton;
        LinearLayout itemsLinearLayout;
        List<EditText> editTextList = new List<EditText>(); // List for all EditText views created to show items' properties to the user
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "Results" layout resource
            SetContentView(Resource.Layout.Results);

            // Set up layout and create list of items
            resultsLinearLayout = FindViewById<LinearLayout>(Resource.Id.resultsLinearLayout);
            
            // Button for adding new items to the list
            resultsNewItemButton = FindViewById<Button>(Resource.Id.resultsNewItemButton);
            resultsSubmitButton = FindViewById<Button>(Resource.Id.resultsSubmitButton);

            // Set up itemsLinearLayout
            itemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.itemsLinearLayout);

            // Get passed receiptText if it's passed, divide it into items and add them as separate TextViews in main LinearLayout
            ProcessReceipt();

            // Add Button and itemsLinearLayout to resultsLinearLayout
            resultsLinearLayout.AddView(resultsNewItemButton);
            resultsLinearLayout.AddView(itemsLinearLayout);

            resultsNewItemButton.Click += OnResultsNewItemButtonClick;
            resultsSubmitButton.Click += OnResultsSubmitButtonClick;
        }

        private void OnResultsSubmitButtonClick(object sender, EventArgs e)
        {
            foreach(var editText in editTextList)
            {
                editText.Clickable = false;
                editText.SetCursorVisible(false);
                editText.Focusable = false;
                editText.FocusableInTouchMode = false;
            }
        }

        private void OnResultsNewItemButtonClick(object sender, EventArgs e)
        {
            Item item = new Item();
            LinearLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
            itemsLinearLayout.AddView(linearLayoutForItem);
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
                LinearLayout itemLinearLayout = NewItem(item);
                itemsLinearLayout.AddView(itemLinearLayout);
            }

        }

        /// <summary>
        /// Creates a LinearLayout with 4 editable EditText views for passed Item object.
        /// Each view is filled with one item property.
        /// </summary>
        private LinearLayout NewItem(Item item)
        {
            LinearLayout itemLayout = new LinearLayout(this);
            itemLayout.LayoutParameters = new LinearLayout.LayoutParams(
                width: ViewGroup.LayoutParams.MatchParent,
                height: ViewGroup.LayoutParams.WrapContent);
            itemLayout.Orientation = Orientation.Horizontal;
            itemLayout.WeightSum = 4;

            EditText itemName = new EditText(this)
            {
                Gravity = GravityFlags.Left,
                Text = item.Name,
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent)
            };
            EditText itemStore = new EditText(this)
            {
                Gravity = GravityFlags.Left,
                Text = item.Store,
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent)
            };
            EditText itemDate = new EditText(this)
            {
                Gravity = GravityFlags.Left,
                Text = item.Date.ToString("yyyy-MM-dd"),
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent)
            };
            EditText itemPrice = new EditText(this)
            {
                Gravity = GravityFlags.Left,
                Text = item.Price.ToString(),
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent)
            };

            //name, date, store, price
            editTextList.Add(itemName);
            editTextList.Add(itemDate);
            editTextList.Add(itemStore);
            editTextList.Add(itemPrice);
            
            itemLayout.AddView(itemName);
            itemLayout.AddView(itemDate);
            itemLayout.AddView(itemStore);
            itemLayout.AddView(itemPrice);

            return itemLayout;
        }
    }
}