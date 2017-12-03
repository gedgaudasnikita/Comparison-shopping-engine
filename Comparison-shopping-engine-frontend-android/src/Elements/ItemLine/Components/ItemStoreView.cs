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
    /// The class that represents the element, responsible for the setting of the store of the receipt
    /// and its behaviour
    /// </summary>
    public class ItemStoreView: EditText
    {
        public StateColorManager StateManager { get; private set; }

        public ItemStoreView(Context ctx, bool editable, String store = null) : base(ctx)
        {
            Hint = AppResources.ItemStoreHint;

            //Review state
            if (store == "")
            {
                StateManager = new StateColorManager(Background.SetTint, ItemInfoStates.INFO_WRONG);
            }
            else
            //Result state
            {
                StateManager = new StateColorManager(Background.SetTint,
                    ItemInfoStates.INFO_CONFIRMED);
            }
            Text = store;

            SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            Clickable = editable;
            Focusable = editable;
            FocusableInTouchMode = editable;

            TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                if (Text == "")
                {
                    StateManager.State = ItemInfoStates.INFO_WRONG;
                }
                else
                {
                    StateManager.State = ItemInfoStates.INFO_UNCERTAIN;
                }
            };
        }

        /// <summary>
        /// The function to check whether the store information is ready to be sent.
        /// Relies on the internal <see cref="StateColorManager"/>s of the elements.
        /// </summary>
        /// <returns>Whether the ItemLine is ready for sending or not</returns>
        public bool Validate()
        {
            return StateManager.State != ItemInfoStates.INFO_WRONG;
        }
    }
}