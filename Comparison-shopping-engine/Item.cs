using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine
{
    public class Item
    {
        private string name;
        private string store;
        //Vienos prakės kaina arba 1kg kaina jei prekė sveriama
        private double price;
        private DateTime date;

        public string Name
        { get; set; }

        public string Store
        { get; set; }

        public double Price
        { get; set; }

        public DateTime Date
        { get; set; }

        public Item(string name, string store, double price, DateTime date)
        {
            this.Name = name;
            this.Store = store;
            this.Price = price;
            this.Date = date;
        }

        public Item(string name, string store, double price, string date)
        {
            this.Name = name;
            this.Store = store;
            this.Price = price;
            this.Date = DateTime.Parse(date);
        }

        public void Print()
        {
            Console.Out.WriteLine(Name + " | " + Store + " | " + Price.ToString() + " | " + Date.ToString());
        }
    }
}
