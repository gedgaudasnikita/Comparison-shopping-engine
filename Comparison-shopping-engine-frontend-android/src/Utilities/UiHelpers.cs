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
using System.Threading.Tasks;

namespace Comparison_shopping_engine_frontend_android
{
    static class UIHelpers
    {
        public static async Task<ResultType> executeWithProgressDialog<ResultType>(Func<IEnumerable<Action<int>>, Task<ResultType>> functionToCall, string title, Context context)
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
    }
}