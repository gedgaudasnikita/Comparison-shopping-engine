using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public class Item : IEquatable<Item>
    {
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
    }
}
