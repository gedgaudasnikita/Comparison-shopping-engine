using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    static public class ItemManager
    {
        //Pasako ar prekių pavadinimai lygūs, palyginimas case-sensitive
        static public bool compareName(Item a, Item b)
        {
            return a.Name.Equals(b.Name, StringComparison.Ordinal);
        }

        //Pasako ar prekių parduotuvių pavadinimai lygūs, palyginimas case-sensitive
        static public bool compareStore(Item a, Item b)
        {
            return a.Store.Equals(b.Store, StringComparison.Ordinal);
        }

        //Palygina prekes pagal kainą ir gražina pigesnę prekę, jeigu kainos vienodos, grąžina prekę a
        static public Item comparePrice(Item a, Item b)
        {
            if (a.Price > b.Price) return b;
            else return a;
        }

        //Palygina prekes pagal datą ir gražina naujesnę prekę, jeigu datos vienodos, grąžina prekę a
        static public Item compareDate(Item a, Item b)
        {
            if (a.Date < b.Date) return b;
            else return a;
        }
    }
}
