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
using static Android.App.ActionBar;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "CoShE Results")]
    public class ResultsActivity : Activity
    {
        LinearLayout resultsLinearLayout;
        Button resultsNewItemButton, resultsSubmitButton;
        LinearLayout itemsLinearLayout;
        TextView topLabel;
        Receipt mainReceipt = new Receipt();
        List<Tuple<EditText, EditText>> editTextLineList = new List<Tuple<EditText, EditText>>(); // List for all EditText views created to show items' properties to the user
        
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
            topLabel = FindViewById<TextView>(Resource.Id.resultsTextView);

            resultsNewItemButton.Click += OnResultsNewItemButtonClick;
            resultsSubmitButton.Click += OnResultsSubmitButtonClick;

            // Set up itemsLinearLayout
            itemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.itemsLinearLayout);

            // Get passed receiptText if it's passed, divide it into items and add them as separate TextViews in main LinearLayout
            DisplayReceipt();

            topLabel.Text = "Did we get it right?";

            resultsSubmitButton.Text = "Looks OK!";
        }

        private async void OnResultsSubmitButtonClick(object sender, EventArgs e)
        {
            Receipt corrected = new Receipt();
            foreach (var editText in editTextLineList)
            {
                //TODO: code smell! need to create different types of editTexts for each thing
                if (editText.Item1.Hint == "Store")
                {
                    corrected.Store = editText.Item1.Text;
                    corrected.Date = DateTime.Parse(editText.Item2.Text);
                } else if (editText.Item1.Hint == "Item Name")
                {
                    corrected.Items.Add(new Item()
                    {
                        Date = corrected.Date,
                        Store = corrected.Store,
                        Name = editText.Item1.Text,
                        Price = Int32.Parse(editText.Item2.Text)
                    });
                }

                editText.Item1.Clickable = false;
                editText.Item1.SetCursorVisible(false);
                editText.Item1.Focusable = false;
                editText.Item1.FocusableInTouchMode = false;
                editText.Item2.Clickable = false;
                editText.Item2.SetCursorVisible(false);
                editText.Item2.Focusable = false;
                editText.Item2.FocusableInTouchMode = false;
            }
            resultsSubmitButton.Visibility = ViewStates.Gone;
            resultsNewItemButton.Visibility = ViewStates.Gone;
            mainReceipt = corrected;

            var result = await UIHelpers.ExecuteWithSpinnerDialog<List<Item>>(async () => {
                    await BackendInterface.SaveReceipt(corrected);
                    return await BackendInterface.ProcessReceipt(corrected);
                }, "Comparing", this
            );
            
            DisplayResults(result);
        }

        private void DisplayResults(List<Item> result)
        {
            topLabel.Text = "Results";
            itemsLinearLayout.RemoveAllViews();

            itemsLinearLayout.AddView(NewStoreDate(mainReceipt.Date, mainReceipt.Store));

            for (int index = 0; index < result.Count; index++)
            {
                var item = result[index];

                foreach (var itemReceipt in mainReceipt.Items)
                {
                    if (itemReceipt.Name != item.Name)
                        continue;

                    RelativeLayout itemRelativeLayout = NewItem(item);
                    itemsLinearLayout.AddView(itemRelativeLayout);

                    var resultText = CreateItemResult(itemReceipt, item);
                    itemsLinearLayout.AddView(resultText);
                }
            }
        }

        private void OnResultsNewItemButtonClick(object sender, EventArgs e)
        {
            Item item = new Item();
            RelativeLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
            itemsLinearLayout.AddView(linearLayoutForItem);
        }

        private TextView CreateItemResult(Item original, Item result)
        {
            RelativeLayout.LayoutParams lpResult = new RelativeLayout.LayoutParams(
                        ViewGroup.LayoutParams.WrapContent,
                        ViewGroup.LayoutParams.WrapContent
                    );

            string output = "";
            Android.Graphics.Color color = Android.Graphics.Color.Black;
            switch (original.Price.CompareTo(result.Price))
            {
                case 0:
                    output = "Couldn't find any cheaper!";
                    color = Android.Graphics.Color.Green;
                    break;
                case -1:
                    output = "You are the first to get it so cheap!";
                    color = Android.Graphics.Color.Blue;
                    break;
                case 1:
                    int difference = original.Price - result.Price;
                    output = $"Could've saved {difference} in {result.Store}!";
                    color = Android.Graphics.Color.Brown;
                    break;
            }

            TextView resultText = new TextView(this)
            {
                Text = output,
                LayoutParameters = lpResult
            };

            resultText.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            resultText.SetTextColor(color);

            return resultText;
        }


        private async void DisplayReceipt()
        {
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? ("");

            resultsNewItemButton.Enabled = false;
            resultsSubmitButton.Enabled = false;

            mainReceipt = await UIHelpers.ExecuteWithSpinnerDialog<Receipt>(async () => {
                    return await BackendInterface.ProcessImage(receiptText);
                }, "Extracting", this
            );

            resultsNewItemButton.Enabled = true;
            resultsSubmitButton.Enabled = true;

            itemsLinearLayout.AddView(NewStoreDate(mainReceipt.Date, mainReceipt.Store));

            foreach(var item in mainReceipt.Items)
            {
                RelativeLayout itemRelativeLayout = NewItem(item);
                itemsLinearLayout.AddView(itemRelativeLayout);
            }
        }

        private RelativeLayout NewStoreDate(DateTime date, string store)
        {
            RelativeLayout storeDateLayout = new RelativeLayout(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent),
            };

            RelativeLayout.LayoutParams lpStore = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpStore.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            EditText storeEdit = new EditText(this)
            {
                Text = store,
                Hint = "Store",
                LayoutParameters = lpStore,
                Id = View.GenerateViewId()
            };

            RelativeLayout.LayoutParams lpDate = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDate.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            EditText dateEdit = new EditText(this)
            {
                Text = date.ToString("yyyy-MM-dd"),
                Hint = "Date",
                LayoutParameters = lpDate,
                Id = View.GenerateViewId()
            };

            dateEdit.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            storeEdit.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            editTextLineList.Add(new Tuple<EditText, EditText> (storeEdit, dateEdit));

            storeDateLayout.AddView(storeEdit);
            storeDateLayout.AddView(dateEdit);

            return storeDateLayout;
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
            };

           /*  RelativeLayout.LayoutParams lpItem = new RelativeLayout.LayoutParams(
                  ViewGroup.LayoutParams.WrapContent,
                  ViewGroup.LayoutParams.WrapContent);
              lpItem.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
              EditText itemName = new EditText(this)
              {
                  Text = item.Name,
                  Hint = "Item Name",
                  LayoutParameters = lpItem,
                  Id = View.GenerateViewId()
              };*/

            RelativeLayout.LayoutParams lpItem = new RelativeLayout.LayoutParams(
                  ViewGroup.LayoutParams.WrapContent,
                  ViewGroup.LayoutParams.WrapContent
            );
            lpItem.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            AutoCompleteTextView itemName = new AutoCompleteTextView(this)
            {
                Text = item.Name,
                Hint = "Item Name",
                LayoutParameters = lpItem,
                Id = View.GenerateViewId()
            };

            itemName.Threshold = 1;
            itemName.DropDownWidth = itemName.Width;
            itemName.TextChanged += (object sender, Android.Text.TextChangedEventArgs a) => { UIHelpers.RenewDropdown(itemName, this); };
            itemName.AfterTextChanged += (object sender, Android.Text.AfterTextChangedEventArgs a) => { itemName.ShowDropDown(); itemName.RefreshDrawableState(); };
            itemName.Click += (object sender, EventArgs a) => { itemName.ShowDropDown(); itemName.RefreshDrawableState(); };

            RelativeLayout.LayoutParams lpPrice = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpPrice.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            EditText itemPrice = new EditText(this)
            {
                Text = item.Price.ToString(),
                Hint = "Price",
                LayoutParameters = lpPrice,
                Id = View.GenerateViewId()
            };

            itemName.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            itemPrice.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            //name, price
            editTextLineList.Add(new Tuple<EditText, EditText>(itemName, itemPrice));

            itemLayout.AddView(itemName);
            itemLayout.AddView(itemPrice);

            return itemLayout;
        }
    }
}