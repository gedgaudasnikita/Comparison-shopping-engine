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
        LinearLayout resultsItemsLinearLayout;
        ItemStoreView storeEdit;
        ItemDateView dateEdit;
        List<ItemLine> itemLineList = new List<ItemLine>(); // List for all EditText views created to show items' properties to the user
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);

            base.OnCreate(savedInstanceState);
            // Set our view from the "Results" layout resource
            SetContentView(Resource.Layout.Results);

            // Set up itemsLinearLayout
            resultsItemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.resultsItemsLinearLayout);
           
            DisplayResults(AppData.receipt.Items);
        }

        private void DisplayResults(List<Item> result)
        {
            resultsItemsLinearLayout.AddView(NewStoreDate(AppData.receipt.Date, AppData.receipt.Store, false));

            for (int index = 0; index < result.Count; index++)
            {
                var item = result[index];

                foreach (var itemReceipt in AppData.receipt.Items)
                {
                    if (itemReceipt.Name != item.Name)
                        continue;

                    LinearLayout itemRelativeLayout = NewItem(itemReceipt, false);
                    resultsItemsLinearLayout.AddView(itemRelativeLayout);

                    var resultText = NewItemResult(itemReceipt, item);
                    resultsItemsLinearLayout.AddView(resultText);
                }
            }
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

                resultsItemsLinearLayout.RemoveView(itemLayout);

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
    }
}