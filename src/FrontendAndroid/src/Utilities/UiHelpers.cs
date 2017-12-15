using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using System.Threading.Tasks;
using Android.Widget;
using Android.Views;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// This static class contains helper methods used to call certain dialogs, pop-ups, or alerts to be used across the application
    /// </summary>
    static class UiHelpers
    {
        /// <summary>
        /// This function is used as a wrapper for async functions, that need to be called with a progress dialog.
        /// Contains the basic settings for a progress dialog. 
        /// Returns the same type that the function passed returns.
        /// </summary>
        /// <typeparam name="ResultType">The type that the function being wrapped returns</typeparam>
        /// <param name="functionToCall">The function being wrapped. Must take an <see cref="IEnumerable"/> of functions that take <see cref="int"/> (indicating progress) as a parameter, needed for progress handling</param>
        /// <param name="title">The <see cref="string"/> title to be displayed on a ProgressDialog</param>
        /// <param name="context">The current <see cref="Android.Content.Context"/> of the application</param>
        /// <returns>The result of the function being wrapped</returns>
        public static async Task<ResultType> ExecuteWithProgressDialog<ResultType>(Func<IEnumerable<Action<int>>, Task<ResultType>> functionToCall, string title, Context context)
        {
            var progressDialog = new ProgressDialog(context);
            progressDialog.SetTitle(title);
            progressDialog.Indeterminate = false;
            progressDialog.Max = 100;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Horizontal);
            progressDialog.SetCancelable(false);
            progressDialog.Show();
            ResultType result = await functionToCall(new List<Action<int>> {(int progress) => { progressDialog.Progress = progress; }});
            progressDialog.Dismiss();
            return result;
        }

        /// <summary>
        /// This function is used as a wrapper for async functions, that need to be called with a spinner dialog.
        /// Contains the basic settings for a spinner dialog. 
        /// Returns the same type that the function passed returns.
        /// </summary>
        /// <typeparam name="ResultType">The type that the function being wrapped returns</typeparam>
        /// <param name="functionToCall">The function being wrapped.</param>
        /// <param name="title">The <see cref="string"/> title to be displayed on a Spinner Dialog</param>
        /// <param name="context">The current <see cref="Android.Content.Context"/> of the application</param>
        /// <returns>The result of the function being wrapped</returns>
        public static async Task<ResultType> ExecuteWithSpinnerDialog<ResultType>(Func<Task<ResultType>> functionToCall, string title, Context context)
        {
            var progressDialog = new ProgressDialog(context);
            progressDialog.SetTitle(title);
            progressDialog.Indeterminate = true;
            progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            progressDialog.SetCancelable(false);
            progressDialog.Show();
            ResultType result = await functionToCall();
            progressDialog.Dismiss();
            return result;
        }

        /// <summary>
        /// Shows a basic <see cref="AlertDialog"/> with a titled closing button.
        /// </summary>
        /// <param name="ctx">The <see cref="Context"/> of the execution</param>
        /// <param name="title">The title of the dialog, optional</param>
        /// <param name="message">The message of the dialog, optional</param>
        /// <param name="closeButton">The name of the button, optional</param>
        public static void ShowDialog(Context ctx, String title = null, String message = null, String closeButton = null)
        {
            var dialog = new AlertDialog.Builder(ctx).Create();

            if (title != null) dialog.SetTitle(title);
            if (message != null) dialog.SetMessage(message);

            if (closeButton != null)
            {
                dialog.SetButton(closeButton, (object sender, DialogClickEventArgs e) =>
                {
                    dialog.Cancel();
                });
            }

            dialog.Show();
        }

        /// <summary>
        /// Shows a basic <see cref="Notification"/>.
        /// </summary>
        /// <param name="ctx">The <see cref="Context"/> of the execution</param>
        /// <param name="title">The content of the notificatoin</param>
        public static void ShowNotification(Context ctx, String content)
        {
            Intent intent = new Intent(ctx, typeof(MainActivity));
            
            PendingIntent pendingIntent =
                PendingIntent.GetActivity(ctx, 0, intent, PendingIntentFlags.OneShot);

            Notification.Builder builder = new Notification.Builder(ctx)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(content)
                .SetContentText("")
                .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                .SetSmallIcon(Resource.Drawable.notification);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                ctx.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
      
        public static RelativeLayout NewStoreDate(Context ctx, ref ItemStoreView storeView, ref ItemDateView dateView, DateTime date, string store, bool clickable = true)
        {
            RelativeLayout storeDateLayout = new RelativeLayout(ctx)
            {
                LayoutParameters = new ViewGroup.LayoutParams(
                    width: ViewGroup.LayoutParams.MatchParent,
                    height: ViewGroup.LayoutParams.WrapContent),
            };

            RelativeLayout.LayoutParams lpStore = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpStore.AddRule(LayoutRules.AlignParentLeft, Convert.ToInt32(true));
            ItemStoreView _storeEdit = new ItemStoreView(ctx, clickable, store)
            {
                LayoutParameters = lpStore,
                Id = View.GenerateViewId()
            };


            RelativeLayout.LayoutParams lpDate = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            lpDate.AddRule(LayoutRules.AlignParentRight, Convert.ToInt32(true));
            ItemDateView _dateEdit = new ItemDateView(ctx, clickable, date)
            {
                LayoutParameters = lpDate,
                Id = View.GenerateViewId()
            };

            storeView = _storeEdit;
            dateView = _dateEdit;

            storeDateLayout.AddView(_storeEdit);
            storeDateLayout.AddView(_dateEdit);

            return storeDateLayout;
        }

        /// <summary>
        /// Creates a RelativeLayout with 4 editable EditText views for passed Item object.
        /// Each view is filled with one item property.
        /// </summary>
        public static LinearLayout NewItem(Context ctx, LinearLayout baseLayout, List<ItemLine> itemLineList, Item item, bool clickable = true)
        {
            LinearLayout itemLayout = new LinearLayout(ctx)
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
            ItemNameView itemName = new ItemNameView(ctx, clickable, item.Name)
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
            ItemPriceView itemPrice = new ItemPriceView(ctx, clickable, item.Price / 100f)
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
            ItemDeleteView deleteItemButton = new ItemDeleteView(ctx)
            {
                Text = "x",
                LayoutParameters = lpDelete,
                Id = View.GenerateViewId()
            };

            //On Button click
            deleteItemButton.Click += (object sender, EventArgs e) =>
            {
                itemLayout.RemoveAllViews();

                baseLayout.RemoveView(itemLayout);

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