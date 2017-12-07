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
    /// <summary>
    /// The class that represents the element, responsible for the setting of the date of the receipt
    /// and its behaviour
    /// </summary>
    public class ItemDateView: EditText
    {
        public StateColorManager StateManager { get; private set; }

        public ItemDateView(Context ctx, bool editable, DateTime date) : base(ctx)
        {
            Hint = AppResources.ItemDateHint;

            Text = date.ToString("yyyy-MM-dd");

            //If editable is set to true, we are in "Results" state
            StateManager = new StateColorManager(Background.SetTint,
                editable ? ItemInfoStates.INFO_UNCERTAIN : ItemInfoStates.INFO_CONFIRMED);

            if (editable)
            {
                //Raise a calendar to pick the date
                Click += (sender, e) => {
                    Clickable = false;
                    DatePickerDialog dialog = new DatePickerDialog(
                        ctx,
                        (object _sender, DatePickerDialog.DateSetEventArgs _e) =>
                        {
                            Text = _e.Date.ToString("yyyy-MM-dd");
                        },
                        date.Year,
                        date.Month,
                        date.Day
                    );
                    dialog.Show();
                    Clickable = true;
                };
            }
            else
            {
                Clickable = false;
            }

            //Avoid the user changing the text in other ways than calendar
            Focusable = false;
            FocusableInTouchMode = false;


            SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
        }
    }
}