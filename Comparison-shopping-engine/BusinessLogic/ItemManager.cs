using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        private string storageDir;

        private ItemManager()
        {
            itemList = new List<Item>();
            storageDir = ConfigurationManager.AppSettings["storageDir"];
        }

        public static ItemManager getInstance()
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

        //Pasako ar sąraše jau yra tokia prekė
        public bool Exists(Item newItem)
        {
            return itemList.Contains(newItem);
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

            IFormatter formatter = new BinaryFormatter();

            foreach(var item in itemList)
            {
                string filename = $"{ storageDir }/{ item.GetHashCode().ToString().PadLeft(10, '0').Substring(0, 10) }.item";
                FileStream fileStream = new FileStream(filename, FileMode.Create);
                formatter.Serialize(fileStream, item);
                fileStream.Close();
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

            IFormatter formatter = new BinaryFormatter();
            if (storageDirInfo.Exists)
            {
                foreach (FileInfo file in storageDirInfo.GetFiles("*.item"))
                {
                    FileStream fileStream = new FileStream(file.FullName, FileMode.Open);
                    itemList.Add((Item)formatter.Deserialize(fileStream));
                    fileStream.Close();
                }
            } else
            {
                System.IO.Directory.CreateDirectory(storageDir);
            }
        }
    }
}
