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
        EditText storeEdit;
        EditText dateEdit;
        TextView topLabel;
        Receipt mainReceipt = new Receipt();
        List<Tuple<AutoCompleteTextView, EditText>> editTextLineList = new List<Tuple<AutoCompleteTextView, EditText>>(); // List for all EditText views created to show items' properties to the user
        
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

            Localise();
            
            resultsNewItemButton.Enabled = true;
            resultsSubmitButton.Enabled = true;
        }

        private void Localise()
        {
            resultsNewItemButton.Text = AppResources.SubmitReceiptButton;
            topLabel.Text = AppResources.SubmitLabel;
            resultsNewItemButton.Text = AppResources.AddNewItemButton;
        }

        private async void OnResultsSubmitButtonClick(object sender, EventArgs e)
        {
            Receipt corrected = new Receipt
            {
                Date = DateTime.Parse(dateEdit.Text),
                Store = storeEdit.Text
            };

            dateEdit.Clickable = false;
            storeEdit.Clickable = false;

            foreach (var editText in editTextLineList)
            {
                corrected.Items.Add(new Item()
                {
                    Date = corrected.Date,
                    Store = corrected.Store,
                    Name = editText.Item1.Text,
                    Price = Int32.Parse(editText.Item2.Text)
                });
            }
            resultsSubmitButton.Visibility = ViewStates.Gone;
            resultsNewItemButton.Visibility = ViewStates.Gone;
            mainReceipt = corrected;

            var result = await UIHelpers.ExecuteWithSpinnerDialog<List<Item>>(async () => {
                    await BackendInterface.SaveReceipt(corrected);
                    return await BackendInterface.ProcessReceipt(corrected);
                }, AppResources.ComparingSpinner, this
            );
            
            DisplayResults(result);
        }

        private void DisplayResults(List<Item> result)
        {
            topLabel.Text = AppResources.ResultLabel;
            itemsLinearLayout.RemoveAllViews();

            itemsLinearLayout.AddView(NewStoreDate(mainReceipt.Date, mainReceipt.Store, false));

            for (int index = 0; index < result.Count; index++)
            {
                var item = result[index];

                foreach (var itemReceipt in mainReceipt.Items)
                {
                    if (itemReceipt.Name != item.Name)
                        continue;

                    RelativeLayout itemRelativeLayout = NewItem(itemReceipt, false);
                    itemsLinearLayout.AddView(itemRelativeLayout);

                    var resultText = NewItemResult(itemReceipt, item);
                    itemsLinearLayout.AddView(resultText);
                }
            }
        }

        private void OnResultsNewItemButtonClick(object sender, EventArgs e)
        {
            Item item = new Item("", 0);
            RelativeLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
            itemsLinearLayout.AddView(linearLayoutForItem);
        }

        private TextView NewItemResult(Item original, Item result)
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
                    output = AppResources.PriceEqual;
                    color = Android.Graphics.Color.Green;
                    break;
                case -1:
                    output = AppResources.PriceSmaller;
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
            string receiptText = Intent.GetStringExtra("ReceiptText") ?? "";

            if (receiptText != "")
            {
                mainReceipt = await UIHelpers.ExecuteWithSpinnerDialog<Receipt>(
                    async () =>
                    {
                        return await BackendInterface.ProcessImage(receiptText);
                    }, 
                    AppResources.ParsingSpinner, 
                    this
                );
            } else
            {
                mainReceipt = new Receipt()
                {
                    Date = DateTime.Today
                };
            }

            itemsLinearLayout.AddView(NewStoreDate(mainReceipt.Date, mainReceipt.Store));

            foreach(var item in mainReceipt.Items)
            {
                RelativeLayout itemRelativeLayout = NewItem(item);
                itemsLinearLayout.AddView(itemRelativeLayout);
            }
        }

        private RelativeLayout NewStoreDate(DateTime date, string store, bool clickable = true)
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
            EditText _storeEdit = new EditText(this)
            {
                Text = store,
                Hint = "Store",
                LayoutParameters = lpStore,
                Id = View.GenerateViewId()
            };

            _storeEdit.Clickable = clickable;
            _storeEdit.Focusable = clickable;
            _storeEdit.FocusableInTouchMode = clickable;

            RelativeLayout.LayoutParams lpDate = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDate.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            
            EditText _dateEdit = new EditText(this)
            {
                Text = date.ToString("yyyy-MM-dd"),
                Hint = "Date",
                LayoutParameters = lpDate,
                Id = View.GenerateViewId()
            };

            if (clickable)
            {
                _dateEdit.Click += (sender, e) => {
                    _dateEdit.Clickable = false;
                    DatePickerDialog dialog = new DatePickerDialog(
                        this,
                        (object _sender, DatePickerDialog.DateSetEventArgs _e) =>
                        {
                            _dateEdit.Text = _e.Date.ToString("yyyy-MM-dd");
                        },
                        date.Year,
                        date.Month,
                        date.Day
                    );
                    dialog.Show();
                    _dateEdit.Clickable = true;
                };
            } else
            {
                _dateEdit.Clickable = false;
            }

            _dateEdit.Focusable = false;
            _dateEdit.FocusableInTouchMode = false;


            _dateEdit.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            _storeEdit.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            storeEdit = _storeEdit;
            dateEdit = _dateEdit;

            storeDateLayout.AddView(_storeEdit);
            storeDateLayout.AddView(_dateEdit);

            return storeDateLayout;
        }

        /// <summary>
        /// Creates a RelativeLayout with 4 editable EditText views for passed Item object.
        /// Each view is filled with one item property.
        /// </summary>
        private RelativeLayout NewItem(Item item, bool clickable = true)
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

            itemName.Clickable = clickable;
            itemName.Focusable = clickable;
            itemName.FocusableInTouchMode = clickable;
            itemName.Threshold = 1;
            itemName.Adapter = new SuggestionAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line);


            RelativeLayout.LayoutParams lpPrice = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);

            //Button that deletes the entire item
            RelativeLayout.LayoutParams lpDelete = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDelete.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            TextView deleteItemButton = new TextView(this)
            {
                Text = "x",
                LayoutParameters = lpDelete,
                Id = View.GenerateViewId()
            };

            if (clickable)
            {
                lpPrice.AddRule(LayoutRules.LeftOf, deleteItemButton.Id);
            }
            else
            {
                lpPrice.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            }

            EditText itemPrice = new EditText(this)
            {
                Text = item.Price.ToString(),
                Hint = "Price",
                LayoutParameters = lpPrice,
                Id = View.GenerateViewId()
            };

            itemPrice.Clickable = clickable;
            itemPrice.Focusable = clickable;
            itemPrice.FocusableInTouchMode = clickable;

            //On Button click
            deleteItemButton.Click += (object sender, EventArgs e) =>
            {
                itemLayout.RemoveAllViews();

                itemsLinearLayout.RemoveView(itemLayout);

                editTextLineList.Remove(new Tuple<AutoCompleteTextView, EditText>(itemName, itemPrice));
            };

            itemName.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            itemPrice.SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
            deleteItemButton.SetTextSize(Android.Util.ComplexUnitType.Pt, 7);
            deleteItemButton.SetTextColor(Android.Graphics.Color.Red);

            //name, price
            editTextLineList.Add(new Tuple<AutoCompleteTextView, EditText>(itemName, itemPrice));

            itemLayout.AddView(itemName);
            itemLayout.AddView(itemPrice);

            if (clickable)
            {
                itemLayout.AddView(deleteItemButton);
            }

            return itemLayout;
        }
    }
}