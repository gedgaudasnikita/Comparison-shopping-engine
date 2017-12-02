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

namespace Comparison_shopping_engine_frontend_android
{
    public static class PopUpDialog
    {
        public static void Show(Context ctx, String title = null, String message = null, String closeButton = null)
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
    }
}