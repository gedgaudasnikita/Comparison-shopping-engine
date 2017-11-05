using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class describes the comparison by price logic for Item classes
    /// </summary>
    public class ItemPriceComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }
}
