using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_frontend_android
{
    [Activity(Label = "CoShE Review")]
    public class ReviewActivity : Activity
    {
        LinearLayout reviewLinearLayout;
        LinearLayout reviewItemsLinearLayout;
        Button reviewNewItemButton;
        Button reviewSubmitButton;
        ItemStoreView storeEdit;
        ItemDateView dateEdit;
        Receipt mainReceipt = new Receipt();
        List<ItemLine> itemLineList = new List<ItemLine>(); // List for all EditText views created to show items' properties to the user

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetTheme(AppData.theme);
            base.OnCreate(savedInstanceState);
            // Set our view from the "Review" layout resource
            SetContentView(Resource.Layout.Review);
            // Set up views
            reviewLinearLayout = FindViewById<LinearLayout>(Resource.Id.reviewLinearLayout);
            reviewItemsLinearLayout = FindViewById<LinearLayout>(Resource.Id.reviewItemsLinearLayout);
            reviewNewItemButton = FindViewById<Button>(Resource.Id.reviewNewItemButton);
            reviewSubmitButton = FindViewById<Button>(Resource.Id.reviewSubmitButton);
            
            reviewNewItemButton.Click += OnReviewNewItemButtonClick;
            reviewSubmitButton.Click += OnReviewSubmitButtonClick;

            reviewNewItemButton.Enabled = true;
            reviewSubmitButton.Enabled = true;

            DisplayReceipt();
        }

        private void OnReviewNewItemButtonClick(object sender, EventArgs e)
        {
            Item item = new Item("", 0);
            LinearLayout linearLayoutForItem = NewItem(item);// passing item with default values because user can change them
            reviewItemsLinearLayout.AddView(linearLayoutForItem);
        }

        private void OnReviewSubmitButtonClick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ResultsActivity));

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

            mainReceipt = corrected;
            AppData.receipt = mainReceipt;

            StartActivity(intent);
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
            }
            else
            {
                mainReceipt = new Receipt()
                {
                    Date = DateTime.Today
                };
            }

            reviewItemsLinearLayout.AddView(NewStoreDate(mainReceipt.Date, mainReceipt.Store));

            foreach (var item in mainReceipt.Items)
            {
                LinearLayout itemRelativeLayout = NewItem(item);
                reviewItemsLinearLayout.AddView(itemRelativeLayout);
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

                reviewItemsLinearLayout.RemoveView(itemLayout);

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