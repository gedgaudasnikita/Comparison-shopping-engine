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
            LinearLayout linearLayoutForItem = UiHelpers.NewItem(this, reviewItemsLinearLayout, itemLineList, item);// passing item with default values because user can change them
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

            reviewItemsLinearLayout.AddView(UiHelpers.NewStoreDate(this, ref storeEdit, ref dateEdit, mainReceipt.Date, mainReceipt.Store));

            foreach (var item in mainReceipt.Items)
            {
                LinearLayout itemRelativeLayout = UiHelpers.NewItem(this, reviewItemsLinearLayout, itemLineList, item);
                reviewItemsLinearLayout.AddView(itemRelativeLayout);
            }
        }
    }
}