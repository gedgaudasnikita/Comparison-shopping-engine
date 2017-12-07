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
    public class ItemNameView: AutoCompleteTextView, AdapterView.IOnItemClickListener
    {
        public StateColorManager StateManager { get; private set; }

        public ItemNameView(Context ctx, bool editable, string name = null) : base(ctx)
        {
            Hint = AppResources.ItemNameHint;

            //we don't get a name if the "New Item" button has been clicked
            if (name == "")
            {
                StateManager = new StateColorManager(Background.SetTint, ItemInfoStates.INFO_WRONG);
            }
            else
            {
                StateManager = new StateColorManager(Background.SetTint, 
                    ItemInfoStates.INFO_CONFIRMED);
            }
            Text = name;

            SetTextSize(Android.Util.ComplexUnitType.Pt, 5);

            Clickable = editable;
            Focusable = editable;
            FocusableInTouchMode = editable;
            Threshold = 1;
            Adapter = new SuggestionAdapter(ctx, Android.Resource.Layout.SimpleDropDownItem1Line);
            OnItemClickListener = this;
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

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Text = Adapter.GetItem(position).ToString();
            StateManager.State = ItemInfoStates.INFO_CONFIRMED;
        }
    }
}