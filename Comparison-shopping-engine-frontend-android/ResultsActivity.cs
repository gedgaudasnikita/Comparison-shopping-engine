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
        LinearLayout resultsLinearLayout;
        Button resultsNewItemButton, resultsSubmitButton;
        LinearLayout itemsLinearLayout;
        List<EditText> editTextList = new List<EditText>(); // List for all EditText views created to show items' properties to the user
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);
            // Set our view from the "Results" layout resource
            SetContentView(Resource.Layout.Results);

            // Set up layout and create list of items
            resultsLinearLayout = FindViewById<LinearLayout>(Resource.Id.resultsLinearLayout);
            
            // Button for adding new items to the list
            resultsNewItemButton = FindViewById<Button>(Resource.Id.resultsNewItemButton);
            resultsSubmitButton = FindViewById<Button>(Resource.Id.resultsSubmitButton);
            resultsNewItemButton.Click += OnResultsNewItemButtonClick;
            resultsSubmitButton.Click += OnResultsSubmitButtonClick;

            // Set up itemsLinearLayout
            itemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.itemsLinearLayout);

            // Get passed receiptText if it's passed, divide it into items and add them as separate TextViews in main LinearLayout
            ProcessReceipt();
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
            RelativeLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
            itemsLinearLayout.AddView(linearLayoutForItem);
        }

        private async void ProcessReceipt()
        {
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? ("");

            BackendInterface backendInterface = new BackendInterface();
            Receipt receiptToProcess = await backendInterface.ProcessImage(receiptText);
            List<Item> itemList = await backendInterface.ProcessReceipt(receiptToProcess);

            foreach(var item in itemList)
            {
                RelativeLayout itemRelativeLayout = NewItem(item);
                itemsLinearLayout.AddView(itemRelativeLayout);
            }

        }

        /// <summary>
        /// Creates a RelativeLayout with 4 editable EditText views for passed Item object.
        /// Each view is filled with one item property.
        /// </summary>
        private RelativeLayout NewItem(Item item)
        {
            RelativeLayout itemLayout = new RelativeLayout(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent),
                Id = View.GenerateViewId()
            };

            RelativeLayout.LayoutParams lpItem = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpItem.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            EditText itemName = new EditText(this)
            {
                Text = item.Name,
                Hint = "Item Name",
                LayoutParameters = lpItem,
                Id = View.GenerateViewId()
            };

            RelativeLayout.LayoutParams lpStore = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpStore.AddRule(LayoutRules.Below, itemName.Id);
            lpStore.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            EditText itemStore = new EditText(this)
            {
                Text = item.Store,
                Hint = "Store Name",
                LayoutParameters = lpStore,
                Id = View.GenerateViewId()
            };
 
            RelativeLayout.LayoutParams lpDate = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDate.AddRule(LayoutRules.Below, itemName.Id);
            lpDate.AddRule(LayoutRules.RightOf, itemStore.Id);
            EditText itemDate = new EditText(this)
            {
                Text = item.Date.ToString("yyyy-MM-dd"),
                Hint = "Date",
                LayoutParameters = lpDate,
                Id = View.GenerateViewId()
            };

            RelativeLayout.LayoutParams lpPrice = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpPrice.AddRule(LayoutRules.Below, itemName.Id);
            lpPrice.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            lpPrice.AddRule(LayoutRules.RightOf, itemDate.Id);
            EditText itemPrice = new EditText(this)
            {
                Text = item.Price.ToString(),
                Hint = "Price",
                LayoutParameters = lpPrice,
                Id = View.GenerateViewId()
            };


            //Button that deletes the entire item
            RelativeLayout.LayoutParams lpDelete = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDelete.AddRule(LayoutRules.Below, itemStore.Id);
            lpDelete.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            Button deleteItemButton = new Button(this)
            {
                Text = "Delete Item",
                LayoutParameters = lpDelete
            };

            //name, date, store, price
            editTextList.Add(itemName);
            editTextList.Add(itemStore);
            editTextList.Add(itemDate);
            editTextList.Add(itemPrice);

            itemLayout.AddView(itemName);
            itemLayout.AddView(itemStore);
            itemLayout.AddView(itemDate);
            itemLayout.AddView(itemPrice);
            itemLayout.AddView(deleteItemButton);

            return itemLayout;
        }
    }
}