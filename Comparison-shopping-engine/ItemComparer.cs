using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    //Statinė klasė turinti visus Item palyginimo metodus
    public static class ItemComparer
    {
        //SameName - sutampa pavadinimai
        //SameStore - sutampa parduotuves
        //SameItem - sutampa pavadinimai ir parduotuves

        //Pasako ar prekių pavadinimai lygūs, palyginimas case-sensitive
        public static bool EqualName(Item a, Item b)
        {
            return a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        //Pasako ar prekių parduotuvių pavadinimai lygūs, palyginimas case-sensitive
        public static bool EqualStore(Item a, Item b)
        {
            return a.Store.Equals(b.Store, StringComparison.Ordinal);
        }

        public static bool IsEqual(Item a, Item b)
        {
            return EqualName(a, b) && EqualStore(a, b) && (a.Price == b.Price) && (a.Date == b.Date);
        }

        public static bool IsSameItemOlder(Item a, Item b)
        {
            return EqualName(a, b) && EqualStore(a, b) && (a.Date < b.Date);
        }

        public static bool IsSameItemNewer(Item a, Item b)
        {
            return EqualName(a, b) && EqualStore(a, b) && (a.Date > b.Date);
        }

        public static bool IsSameNameCheaper(Item a, Item b)
        {
            return EqualName(a, b) && (a.Price > b.Price);
        }

        public static bool IsCheaper(Item a, Item b)
        {
            return a.Price < b.Price;
        }
    }
}