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
        ItemStoreView storeEdit;
        ItemDateView dateEdit;
        TextView topLabel;
        Receipt mainReceipt = new Receipt();
        List<ItemLine> itemLineList = new List<ItemLine>(); // List for all EditText views created to show items' properties to the user
        
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
            if (itemLineList.Count == 0)
            {
                UiHelpers.ShowDialog(this,
                    AppResources.SubmitNoItemsTitle,
                    AppResources.SubmitNoItemsMessage,
                    AppResources.SubmitNoItemsButton);
                return;
            }

            Receipt corrected = new Receipt
            {
                Date = DateTime.Parse(dateEdit.Text),
                Store = storeEdit.Text
            };

            dateEdit.Clickable = false;
            storeEdit.Clickable = false;

            bool validated = storeEdit.Validate();
            foreach (var itemLine in itemLineList)
            {
                if (!itemLine.Validate())
                {
                    validated = false;
                    continue;
                }
                corrected.Items.Add(new Item()
                {
                    Date = corrected.Date,
                    Store = corrected.Store,
                    Name = itemLine.Name.Text,
                    Price = Int32.Parse(itemLine.Price.GetNormalizedText())
                });
            }

            if (!validated)
            {
                UiHelpers.ShowDialog(this, 
                    AppResources.SubmitErrorTitle, 
                    AppResources.SubmitErrorMessage,
                    AppResources.SubmitErrorButton);
                return;
            }

            resultsSubmitButton.Visibility = ViewStates.Gone;
            resultsNewItemButton.Visibility = ViewStates.Gone;
            mainReceipt = corrected;

            var result = await UiHelpers.ExecuteWithSpinnerDialog<List<Item>>
            (
                async () => {
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

                    LinearLayout itemRelativeLayout = NewItem(itemReceipt, false);
                    itemsLinearLayout.AddView(itemRelativeLayout);

                    var resultText = NewItemResult(itemReceipt, item);
                    itemsLinearLayout.AddView(resultText);
                }
            }
        }

        private void OnResultsNewItemButtonClick(object sender, EventArgs e)
        {
            Item item = new Item("", 0);
            LinearLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
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
                    output = String.Format(AppResources.PriceLarger, difference, result.Store);
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
                mainReceipt = await UiHelpers.ExecuteWithSpinnerDialog<Receipt>(
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
                LinearLayout itemRelativeLayout = NewItem(item);
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
            ItemStoreView _storeEdit = new ItemStoreView(this, clickable, store)
            {
                LayoutParameters = lpStore,
                Id = View.GenerateViewId()
            };


            RelativeLayout.LayoutParams lpDate = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDate.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            ItemDateView _dateEdit = new ItemDateView(this, clickable, date)
            {
                LayoutParameters = lpDate,
                Id = View.GenerateViewId()
            };

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
        private LinearLayout NewItem(Item item, bool clickable = true)
        {
            LinearLayout itemLayout = new LinearLayout(this)
            {
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent
                ),
                Id = View.GenerateViewId(),
                WeightSum = 1f
            };

            LinearLayout.LayoutParams lpItem = new LinearLayout.LayoutParams(
                  ViewGroup.LayoutParams.WrapContent,
                  ViewGroup.LayoutParams.WrapContent,
                  0.80f
            )
            {
                Width = 0,
                Gravity = GravityFlags.Fill
            };
            ItemNameView itemName = new ItemNameView(this, clickable, item.Name)
            {
                LayoutParameters = lpItem,
                Id = View.GenerateViewId()
            };

            LinearLayout.LayoutParams lpPrice = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent,
                clickable ? 0.15f : 0.25f
            )
            {
                Width = 0,
                Gravity = GravityFlags.Fill
            };
            ItemPriceView itemPrice = new ItemPriceView(this, clickable, item.Price / 100f)
            {
                LayoutParameters = lpPrice,
                Id = View.GenerateViewId()
            };
            
            //Button that deletes the entire item
            LinearLayout.LayoutParams lpDelete = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent,
                0.5f
            );
            ItemDeleteView deleteItemButton = new ItemDeleteView(this)
            {
                Text = "x",
                LayoutParameters = lpDelete,
                Id = View.GenerateViewId()
            };

            //On Button click
            deleteItemButton.Click += (object sender, EventArgs e) =>
            {
                itemLayout.RemoveAllViews();

                itemsLinearLayout.RemoveView(itemLayout);

                itemLineList.Remove(new ItemLine()
                {
                    Name = itemName,
                    Price = itemPrice
                });
            };
            
            itemLineList.Add(new ItemLine()
            {
                Name = itemName,
                Price = itemPrice
            });

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