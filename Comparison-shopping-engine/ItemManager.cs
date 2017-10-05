using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    //Klasė atsakinga už Item objektų sarašą, jo tvarkymą ir jo rašymą/skaitymą iš failo
    //Pradžiai naudoju singleton'ą, kuris laiko List<Item>, tad visas prekių sąrašas bus laikomas atmintį
    //Singleton'o sūkurimas neapsaugotas nuo dvigubo sukurimo tarp skirtingų thread'ų, todėl jį sukurti turėtų tik vienas thread'as
    public class ItemManager
    {
        private static ItemManager instance = null;
        private List<Item> itemList;

        private ItemManager()
        {
                itemList = new List<Item>();
        }

        public static ItemManager Init()
        {
            if (instance == null)
            {
                instance = new ItemManager();
            }
            return instance;
        }

        //Ideda prekę į sąrašą
        public void AddItem(Item item)
        {
            itemList.Add(item);
        }

        //Palygina prekę su visu sąrašu
        //Jeigu prekės neranda, įdeda ją į sąrašą
        //Jeigu randą senesnę su kitokia kaina, pakeičia ją naujesne
        public void CompareAddItem(Item newItem)
        {
            if (!itemList.Contains(newItem))
            {
                AddItem(newItem);
            }
        }

        //Grąžina pigiausią prekę su tokių pat pavadinimu
        public Item FindCheapest(Item newItem)
        {
            foreach (Item oldItem in instance.itemList)
            {
                if (new ItemNameComparer().Compare(oldItem, newItem) == 0 && new ItemPriceComparer().Compare(oldItem, newItem) < 0)
                    newItem = oldItem;

            }
            return newItem;
        }

        public int Count()
        {
            return itemList.Count();
        }

        public void ClearList()
        {
            itemList.Clear();
        }
    }
}
