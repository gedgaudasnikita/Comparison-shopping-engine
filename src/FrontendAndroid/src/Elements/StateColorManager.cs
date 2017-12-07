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
    public enum ItemInfoStates { INFO_WRONG, INFO_UNCERTAIN, INFO_CONFIRMED };

    /// <summary>
    /// This class is used to manage the information states in ItemLines, map respective actions to the
    /// state changes and encapsulates the responsibility for general state management.
    /// </summary>
    public class StateColorManager
    {
        private ItemInfoStates state;
        private Action<int> colorer;

        /// <summary>
        /// The current State of the Manager. On each change of State, the appropriate action is executed.
        /// </summary>
        public ItemInfoStates State {
            get
            {
                return state;
            }
            set
            {
                Android.Graphics.Color color = Android.Graphics.Color.Black;
                state = value;

                switch (state)
                {
                    case ItemInfoStates.INFO_CONFIRMED:
                        color = Android.Graphics.Color.Green;
                        break;
                    case ItemInfoStates.INFO_UNCERTAIN:
                        color = Android.Graphics.Color.Blue;
                        break;
                    case ItemInfoStates.INFO_WRONG:
                        color = Android.Graphics.Color.Red;
                        break;
                }
                colorer(color);
            }
        }
        
        public StateColorManager(Action<int> _colorer, ItemInfoStates def = ItemInfoStates.INFO_UNCERTAIN)
        {
            colorer = _colorer;
            State = def;
        }
    }
}