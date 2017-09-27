using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    //Statinė klasė turinti visus Item palyginimo metodus
    static public class ItemComparer
    {
        //Pasako ar prekių pavadinimai lygūs, palyginimas case-sensitive
        static public bool EqualName(Item a, Item b)
        {
            return a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        //Pasako ar prekių parduotuvių pavadinimai lygūs, palyginimas case-sensitive
        static public bool EqualStore(Item a, Item b)
        {
            return a.Store.Equals(b.Store, StringComparison.Ordinal);
        }

        //Palygina prekes pagal kainą ir gražina pigesnę prekę, jeigu kainos vienodos, grąžina prekę a
        static public Item ComparePrice(Item a, Item b)
        {
            if (a.Price > b.Price) return b;
            else return a;
        }

        //Palygina prekes pagal datą ir gražina naujesnę prekę, jeigu datos vienodos, grąžina prekę a
        static public Item CompareDate(Item a, Item b)
        {
            if (a.Date < b.Date) return b;
            else return a;
        }
    }
}