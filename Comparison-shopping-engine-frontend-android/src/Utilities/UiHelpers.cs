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
    /// <summary>
    /// This static class contains helper methods used to call certain dialogs, pop-ups, or alerts to be used across the application
    /// </summary>
    static class UIHelpers
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

        public static async void RenewDropdown(AutoCompleteTextView view, Context ctx)
        {
            var autoCompleteOptions = await BackendInterface.GetSuggestions(view.Text, 10);
            view.Adapter = new ArrayAdapter(ctx, Android.Resource.Layout.SimpleListItem1, autoCompleteOptions);
        }
    }
}