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

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// The class that represents the element, responsible for the setting of the price of the ItemLine
    /// and its behaviour
    /// </summary>
    public class ItemPriceView : EditText
    {
        private float price = 0;

        public StateColorManager StateManager { get; private set; }

        public ItemPriceView(Context ctx, bool editable, float _price = 0f) : base(ctx)
        {
            price = _price;

            Hint = AppResources.ItemPriceHint;
            SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            Console.WriteLine(editable);

            StateManager = new StateColorManager(Background.SetTint, 
                editable ? ItemInfoStates.INFO_UNCERTAIN : ItemInfoStates.INFO_CONFIRMED);
                
            Clickable = editable;
            Focusable = editable;
            FocusableInTouchMode = editable;

            FocusChange += Normalise;
            if (editable)
            {
                TextChanged += Validate;
                Validate(null, null);
            }


            Normalise(null, new FocusChangeEventArgs(false));
        }

        /// <summary>
        /// A function to be called on each change to the focus of the field. If there is no focus,
        /// it adjusts the formatting of the field to a standard float representation
        /// </summary>
        /// <param name="sender">An object to invoke a <see cref="View.FocusChange"/> event</param>
        /// <param name="e">The arguments of the <see cref="View.FocusChange"/> event</param>
        private void Normalise(object sender, FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                String tmp = price.ToString("0.00");
                Text = tmp;
            }
        }

        /// <summary>
        /// A function to be called on each change to the text. Checks if the entered value can be converted
        /// to a float, and sets the state respectively.
        /// </summary>
        /// <param name="sender">An object to invoke a <see cref="TextView.TextChanged"/> event</param>
        /// <param name="e">The arguments of the <see cref="TextView.TextChanged"/> event</param>
        private void Validate(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (Text != "" && float.TryParse(Text, out price))
            {
                StateManager.State = ItemInfoStates.INFO_UNCERTAIN;
            }
            else
            {
                StateManager.State = ItemInfoStates.INFO_WRONG;
            }
        }

        /// <summary>
        /// The function to get the Price value, prepared for sending to the backend
        /// </summary>
        /// <returns>Digits-only <see cref="String"/> with the entered price</returns>
        public String GetNormalizedText()
        {
            return Text.RemoveNonDigits();
        }
    }
}