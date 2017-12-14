using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// Class containing all the methods used to call Database
    /// </summary>
    public static class DBManager
    {
        /// <summary>
        /// Checks if there already exists a cheaper entry for GivenItem in the Database
        /// If item exists, given item is added as an ItemHistory
        /// Else if the existing item is more expensive, adds given item to Database and transfers old Item to ItemHistory
        /// Else adds it to the DB
        /// </summary>
        /// <param name="newItem">An <see cref = "Item"/> to add to the DB</param>
        public static void Add(Item newItem)
        {
            using (ItemsContext db = new ItemsContext())
            {
                if (db.Items.Any(i => i.Name.Equals(newItem.Name)))
                {
                    if (db.Items.Any(i => i.Name.Equals(newItem.Name) && i.Price <= newItem.Price))
                    {
                        if (!db.ItemHistories.Any(i => i.Name.Equals(newItem.Name) && i.Store.Equals(newItem.Store) && i.Price == newItem.Price && i.Date == newItem.Date))
                            db.ItemHistories.Add(new DBItemHistory(newItem));
                    }
                    else
                    {
                        db.ItemHistories.Add(new DBItemHistory((from item in db.Items
                                                                where item.Name.Equals(newItem.Name)
                                                                select item).First()));

                        DBItem itemToChange = (from item in db.Items
                                              where item.Name.Equals(newItem.Name)
                                              select item).First();

                        itemToChange.Name = newItem.Name;
                        itemToChange.Store = newItem.Store;
                        itemToChange.Price = newItem.Price;
                        itemToChange.Date = newItem.Date;
                    }

                }
                else db.Items.Add(new DBItem(newItem));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Checks if an item exists for each given item in given list
        /// If item exists, given item is added as an ItemHistory
        /// Else if the existing item is more expensive, adds given item to DB and transfers old Item to ItemHistory
        /// Else adds it to the DB
        /// </summary>
        /// <param name="newItems">A list of <see cref = "Item"/> to add to the DB</param>
        public static void Add(List<Item> newItems)
        {
            foreach (Item newItem in newItems)
                Add(newItem);
        }

        /// <summary>
        /// Returns the cheapest <see cref = "Item"/> of the same name from the Database
        /// </summary>
        /// <param name="newItem">An <see cref = "Item"/> to compare to</param>
        /// <returns></returns>
        public static Item FindCheapest(Item newItem)
        {
            using (ItemsContext db = new ItemsContext())
            {
                if (db.Items.Any(i => i.Name.Equals(newItem.Name) && i.Price < newItem.Price))
                {
                    DBItem DBItem = (from item in db.Items
                                     where item.Name.Equals(newItem.Name)
                                     select item).First();
                    return new Item(DBItem.Name, DBItem.Store, DBItem.Price, DBItem.Date);
                }
                else return newItem;
            }
        }

        //These methods are used for Unit tests and other testing purposes
        /// <summary>
        /// Removes given item from Database and replaces it with the cheapest of its ItemHistories if it has any
        /// </summary>
        /// <param name="newItem"> An <see cref = "Item"/> to delete</param>
        public static void Remove(Item newItem)
        {
            using (ItemsContext db = new ItemsContext())
            {
                if (db.Items.Any(i => i.Name.Equals(newItem.Name) && i.Store.Equals(newItem.Store) && i.Price == newItem.Price && i.Date == newItem.Date))
                {
                    if (db.ItemHistories.Any(i => i.Name.Equals(newItem.Name)))
                    {
                        DBItemHistory itemToRemove = (from item in db.ItemHistories
                                                      where item.Name.Equals(newItem.Name)
                                                      orderby item.Price ascending
                                                      select item).First();

                        DBItem itemToReplace = db.Items.First(i => i.Name.Equals(itemToRemove.Name));
                        itemToReplace.Store = itemToRemove.Store;
                        itemToReplace.Price = itemToRemove.Price;
                        itemToReplace.Date = itemToRemove.Date;

                        db.ItemHistories.Remove(itemToRemove);

                    }
                    else
                    {
                        DBItem itemToRemove = new DBItem(newItem);
                        db.Items.Remove(itemToRemove);
                    }
                    db.SaveChanges();
                }
            }
        }

        public static bool Contains(Item newItem)
        {
            using (ItemsContext db = new ItemsContext())
            {
                return db.Items.Any(i => i.Name.Equals(newItem.Name) && i.Store.Equals(newItem.Store) && i.Price == newItem.Price && i.Date == newItem.Date);
            }
        }

        /// <summary>
        /// WARNING!
        /// Clears the entire Database
        /// </summary>
        public static void ClearDB()
        {
            using (ItemsContext db = new ItemsContext())
            {
                var ItemQuery = from item in db.Items
                                select item;
                foreach (var item in ItemQuery)
                    db.Items.Remove(item);

                var ItemHistoryQuery = from item in db.ItemHistories
                                       select item;
                foreach (var item in ItemHistoryQuery)
                    db.ItemHistories.Remove(item);

                db.SaveChanges();
            }
        }

        /// <summary>
        /// Adds old data to Database
        /// Should only be used once, ever
        /// </summary>
        public static void SetupDB()
        {
            //Add old items to DB
            List<Item> items = new List<Item>();
            items.Add(new Item("KELMES pienas, 2,5% rieb.", "IKI", 10, DateTime.Now.Date));
            items.Add(new Item("Malta kava Paulig Extra“", "MAXIMA", 899, DateTime.Now.Date));
            items.Add(new Item("Ledai Maxima Favorit", "MAXIMA", 129, DateTime.Now.Date));

            Add(items);
        }

        /// <summary>
        /// Prints the entire Database to console
        /// </summary>
        public static void PrintDB()
        {
            using (var db = new ItemsContext())
            {
                var itemQuery = from i in db.Items
                                select i;

                Console.Out.WriteLine("All the Items:");
                foreach (var item in itemQuery)
                    Console.Out.WriteLine(item.ToString());

                var itemHistoryQuery = from i in db.ItemHistories
                                       select i;

                Console.Out.WriteLine("\nAll the ItemHistories:");
                foreach (var item in itemHistoryQuery)
                    Console.Out.WriteLine(item.ToString());
            }
        }
    }
}
