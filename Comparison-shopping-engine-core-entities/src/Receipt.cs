using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// A class representing the Receipt entity.
    /// </summary>
    public class Receipt
    {
        //Instance fields
        //User provided data, encapsulated in one entity\

        public string Store { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Date { get; set; }
        
        public Receipt()
        {
            Store = "";
            Date = new DateTime().Date;
            Items = new List<Item>();
        }
    }
}
