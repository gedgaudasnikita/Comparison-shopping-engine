using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class describes the comparison by store logic for Item classes
    /// </summary>
    public class ItemStoreComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Store.CompareTo(y.Store);
        }
    }
}
