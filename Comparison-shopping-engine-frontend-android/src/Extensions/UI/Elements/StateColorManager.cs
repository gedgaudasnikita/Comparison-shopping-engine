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

    public class StateColorManager
    {
        private ItemInfoStates state;
        private Action<int> colorer;

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

                Console.WriteLine(state);

                Console.WriteLine(Android.Graphics.Color.Black);
                Console.WriteLine(Android.Graphics.Color.Green);
                Console.WriteLine(Android.Graphics.Color.Blue);
                Console.WriteLine(Android.Graphics.Color.Red);
                Console.WriteLine((int)color);
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