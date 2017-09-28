﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    //Klasė atsakinga už Item objektų sarašą, jo tvarkymą ir jo rašymą/skaitymą iš failo
    //Pradžiai naudoju singleton'ą, kuris laiko List<Item>, tad visas prekių sąrašas bus laikomas atmintį
    //Singleton'o sūkurimas neapsaugotas tarp nuo skirtingų thread'ų, todėl jį sukurti turėtų tik vienas thread'as
    public class ItemManager
    {
        private static ItemManager instance = null;
        private List<Item> itemList;

        private ItemManager() {}

        public static ItemManager Init()
        {
            if (instance == null)
            {
                instance = new ItemManager();
                instance.itemList = new List<Item>();
            }

            return instance;
        }

        //Ideda prekę į sąrašą
        public void AddItem(Item item)
        {
            if (instance != null)
                instance.itemList.Add(item);
            else Console.Out.WriteLine("ItemManager not initialised!");
        }

        //Palygina prekę su visu sąrašu
        //Jeigu prekės neranda, įdeda ją į sąrašą
        //Jeigu randą senesnę su kitokia kaina, pakeičia ją naujesne
        public void CompareAddItem(Item newItem)
        {
            bool inList = false;
            foreach (Item oldItem in instance.itemList)
            {
                if (ItemComparer.IsEqual(newItem, oldItem))
                {
                    inList = true;
                    Console.Out.WriteLine("Item already exists in list");
                    break;
                }

                else if (ItemComparer.IsSameItemOlder(newItem, oldItem))
                {
                    inList = true;
                    Console.Out.WriteLine("Newer same Item already exists in list");
                    break;
                }

                else if (ItemComparer.IsSameItemNewer(newItem, oldItem))
                {
                    instance.itemList.Remove(oldItem);
                    AddItem(newItem);
                    Console.Out.WriteLine("Older Item found, replaced with new Item");
                    inList = true;
                    break;
                }
            }
            if (!inList)
            {
                AddItem(newItem);
                Console.Out.WriteLine("No Item found, Item added to list");
            }
        }

        //Grąžina pigiausią prekę su tokių pat pavadinimu
        public Item FindCheaper(Item newItem)
        {
            CompareAddItem(newItem);
            foreach (Item oldItem in instance.itemList)
            {
                if (ItemComparer.IsSameItemOlder(newItem, oldItem) && !ItemComparer.IsCheaper(newItem, oldItem))
                    newItem = oldItem;

                else if (ItemComparer.IsSameNameCheaper(newItem, oldItem))
                {
                    newItem = oldItem;
                }

            }
            return newItem;
        }

        public void PrintList()
        {
            foreach (Item item in instance.itemList)
                item.Print();
        }
    }
}
