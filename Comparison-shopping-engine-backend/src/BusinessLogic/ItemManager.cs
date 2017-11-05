using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    //Klasė atsakinga už Item objektų sarašą, jo tvarkymą ir jo rašymą/skaitymą iš failo
    //Pradžiai naudoju singleton'ą, kuris laiko List<Item>, tad visas prekių sąrašas bus laikomas atmintį
    //Singleton'o sūkurimas neapsaugotas nuo dvigubo sukurimo tarp skirtingų thread'ų, todėl jį sukurti turėtų tik vienas thread'as
    /// <summary>
    /// Class responsible for managing an internal List<Item>, as well as writing/reading from file
    /// ItemManager itself is a singleton, so the entire List is in the memory for now
    /// Singleton initialisation is not double thread protected
    /// </summary>
    public class ItemManager
    {
        private static ItemManager instance = null;
        private List<Item> itemList;
        private string storageDir;

        private ItemManager()
        {
            itemList = new List<Item>();
            storageDir = ConfigurationManager.AppSettings["storageDir"];
        }

        /// <summary>
        /// Initializes ItemManager and return reference to ItemManager object
        /// </summary>
        /// <returns></returns>
        public static ItemManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ItemManager();
            }
            return instance;
        }

        /// <summary>
        /// Adds a given <see cref = "Item"/> to ItemManagers internal List
        /// </summary>
        /// <param name="item">An <see cref = "Item"/> to add to the list</param>
        public void Add(Item item)
        {
            item.Saved = true;
            itemList.Add(item);
        }

        /// <summary>
        /// Adds a given <see cref = "List{Item}"/> to ItemManagers internal List
        /// </summary>
        /// <param name="list">A <see cref = "List{Item}"> to add Items from</param>
        public void Add(List<Item> list)
        {
            foreach (Item item in list)
                Add(item);
        }

        /// <summary>
        /// Checks if a given item already exists in the list
        /// </summary>
        /// <param name="newItem">An <see cref = "Item"/> to check for</param>
        /// <returns></returns>
        public bool Exists(Item newItem)
        {
            return itemList.Contains(newItem);
        }

        /// <summary>
        /// Returns the cheapest <see cref = "Item"/> of the same name from the list
        /// </summary>
        /// <param name="newItem">An <see cref = "Item"/> to compare to</param>
        /// <returns></returns>
        public Item FindCheapest(Item newItem)
        {
            foreach (Item oldItem in instance.itemList)
            {
                if (new ItemNameComparer().Compare(oldItem, newItem) == 0 && new ItemPriceComparer().Compare(oldItem, newItem) < 0)
                    newItem = oldItem;

            }
            return newItem;
        }

        /// <summary>
        /// Returns the amount of items in itemList
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return itemList.Count();
        }

        /// <summary>
        /// Runs Clear() on internal itemList
        /// </summary>
        public void ClearList()
        {
            itemList.Clear();
        }

        /// <summary>
        /// A method serializing all of the Item objects currently stored
        /// to a User configured location.
        /// Deletes all old *.item files in the named directory before writing.
        /// </summary>
        public void Persist()
        {
            DirectoryInfo storageDirInfo = new DirectoryInfo(storageDir);

            if (storageDirInfo.Exists)
            {
                foreach (FileInfo file in storageDirInfo.GetFiles())
                {
                    file.Delete();
                }
            } else
            {
                System.IO.Directory.CreateDirectory(storageDir);
            }

            JsonSerializerStream serializer = new JsonSerializerStream();

            foreach (var item in itemList)
            {
                string filename = $"{ storageDir }/{ item.GetHashCode().ToString().PadLeft(10, '0').Substring(0, 10) }.item";
                FileStream resultStream = new FileStream(filename, FileMode.OpenOrCreate);
                serializer.Serialize(resultStream, item);
                resultStream.Close();
            }
        }

        /// <summary>
        /// A method deserializing all of the Item objects currently saved
        /// from a User configured location.
        /// Deletes all old Item objects from itemList before reading.
        /// </summary>
        public void LoadAll()
        {
            itemList.Clear();

            DirectoryInfo storageDirInfo = new DirectoryInfo(storageDir);

            JsonSerializerStream serializer = new JsonSerializerStream();
            if (storageDirInfo.Exists)
            {
                foreach (FileInfo file in storageDirInfo.GetFiles("*.item"))
                {
                    FileStream inputStream = new FileStream(file.FullName, FileMode.OpenOrCreate);
                    var item = serializer.Deserialize<Item>(inputStream);
                    itemList.Add(item);
                    inputStream.Close();
                }
            } else
            {
                System.IO.Directory.CreateDirectory(storageDir);
            }
        }
    }
}
