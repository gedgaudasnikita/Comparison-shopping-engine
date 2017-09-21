using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    static public class ItemManager
    {
        //Palygina dvi prekes ir grąžiną pigesnę, jeigu kainos sutampa grąžina prekę a
        static public Item comparePrice (Item a, Item b)
        {
            if (b.Price < a.Price) return b;
            else return a;
        }
    }
}
