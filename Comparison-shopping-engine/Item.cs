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

        public string Name
        {
            get; set;
        }

        public string Store
        {
            get; set;
        }

        public double Price
        {
            get; set;
        }

        public Item()
        {
            this.name = "";
            this.store = "";
            this.price = 0;
        }

        public Item(string name, string store, double price)
        {
            this.name = name;
            this.store = store;
            this.price = price;
        }

        public void print()
        {
            Console.Out.WriteLine(name + " " + store + " " + price.ToString());
        }
    }
}
