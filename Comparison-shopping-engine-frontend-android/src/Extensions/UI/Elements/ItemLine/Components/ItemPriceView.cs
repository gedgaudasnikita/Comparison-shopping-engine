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
    public class ItemPriceView : EditText
    {
        private float price = 0;

        public StateColorManager StateManager { get; private set; }

        public ItemPriceView(Context ctx, bool editable, float _price = 0f) : base(ctx)
        {
            price = _price;

            Hint = "Price";
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

        private void Normalise(object sender, FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
            {
                String tmp = price.ToString("0.00");
                Text = tmp;
            }
        }

        public void Validate(object sender, Android.Text.TextChangedEventArgs e)
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

        public String GetNormalizedText()
        {
            return Text.RemoveNonDigits();
        }
    }
}