using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public class ItemNameComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }

    public class ItemStoreComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Store.CompareTo(y.Store);
        }
    }

    public class ItemPriceComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Price.CompareTo(y.Price);
        }
    }

    public class ItemDateComparer : IComparer<Item>
    {
        public int Compare(Item x, Item y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}