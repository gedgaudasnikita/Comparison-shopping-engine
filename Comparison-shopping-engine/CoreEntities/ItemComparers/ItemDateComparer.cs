using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    /// <summary>
    /// This class describes the comparison by date logic for Item classes
    /// </summary>
    public class ItemDateComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
