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
    public class ItemDateView: EditText
    {
        public StateColorManager StateManager { get; private set; }

        public ItemDateView(Context ctx, bool editable, DateTime date) : base(ctx)
        {
            Hint = "Date";

            if (date == null)
            {
                Text = "";
                StateManager = new StateColorManager(Background.SetTint, ItemInfoStates.INFO_UNCERTAIN);
            }
            else
            {
                Text = date.ToString("yyyy-MM-dd");
                StateManager = new StateColorManager(Background.SetTint,
                    editable ? ItemInfoStates.INFO_UNCERTAIN : ItemInfoStates.INFO_CONFIRMED);
            }

            if (editable)
            {
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

            Focusable = false;
            FocusableInTouchMode = false;


            SetTextSize(Android.Util.ComplexUnitType.Pt, 5);
        }
    }
}