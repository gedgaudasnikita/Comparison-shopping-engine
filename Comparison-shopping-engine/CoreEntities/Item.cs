using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    /// <summary>
    /// The class, representing the core item entity.
    /// </summary>
    [Serializable]
    public class Item : IEquatable<Item>, ISerializable
    {
        //Indicates whether the Item has been saved to the storage or not
        public bool Saved
        { get; set; }
        public string Name
        { get; set; }

        public string Store
        { get; set; }

        public int Price
        { get; set; }

        public DateTime Date
        { get; set; }

        public Item(string name)
        {
            this.Name = name;
            this.Store = "MissingSt";
            this.Price = Int32.MaxValue;
            this.Date = DateTime.Now.Date;
        }

        public Item(string name, string store)
            : this(name)
        {
            this.Store = store;
        }

        public Item(string name, string store, int price)
            : this(name, store)
        {
            this.Price = price;
        }

        public Item(string name, int price)
            : this(name)
        {
            this.Price = price;
            this.Store = "MissingSt";

        }

        public Item(string name, int price, DateTime date)
            : this(name, price)
        {
            this.Date = date;
        }

        public Item(string name, int price, string date)
            : this(name, price)
        {
            this.Date = DateTime.Parse(date);
        }

        public Item(string name, string store, int price, DateTime date)
            :this(name, store, price)
        {
            this.Date = date;
        }

        public Item(string name, string store, int price, string date)
            :this(name, store, price)
        {
            this.Date = DateTime.Parse(date);
        }

        override public String ToString()
        {
            return (Name + " | " + Store + " | " + Price.ToString() + " | " + Date.ToString());
        }

        public bool Equals(Item other)
        {
            return (new ItemNameComparer().Compare(this, other) == 0
                && new ItemStoreComparer().Compare(this, other) == 0
                && new ItemPriceComparer().Compare(this, other) == 0
                && new ItemDateComparer().Compare(this, other) == 0);
        }

        /// <summary>
        /// Checks whether the data contained is valid. Used to determined whether the Item object
        /// is ready to be saved to storage or not.
        /// </summary>
        /// <returns>The <see langword="bool"/> result, indicating whether the data is valid or not</returns>
        public bool isComplete()
        {
            return (Store != "MissingSt" && Store != "") && (Price != 0);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name, typeof(string));
            info.AddValue("Price", Price, typeof(int));
            info.AddValue("Store", Store, typeof(string));
            info.AddValue("Date", Date, typeof(DateTime));
        }

        public Item(SerializationInfo info, StreamingContext context)
        {
            Name = (string)info.GetValue("Name", typeof(string));
            Price = (int)info.GetValue("Price", typeof(int));
            Store = (string)info.GetValue("Store", typeof(string));
            Date = (DateTime)info.GetValue("Date", typeof(DateTime));
        }
    }
}
