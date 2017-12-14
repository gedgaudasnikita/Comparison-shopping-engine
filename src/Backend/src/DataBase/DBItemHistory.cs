using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_backend
{
    public class DBItemHistory
    {
        [Key]
        [Column(Order = 2)]
        public string Store { get; set; }
        [Key]
        [Column(Order = 3)]
        public int Price { get; set; }
        [Key]
        [Column(Order = 4)]
        public DateTime Date { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Item")]
        public string Name { get; set; }
        public virtual DBItem Item { get; set; }

        public DBItemHistory()
        {
        }

        public DBItemHistory(Item item)
        {
            Name = item.Name;
            Store = item.Store;
            Price = item.Price;
            Date = item.Date;
        }

        public DBItemHistory(DBItem item)
        {
            Name = item.Name;
            Store = item.Store;
            Price = item.Price;
            Date = item.Date;
        }

        override public string ToString()
        {
            return Name + " " + Store + " " + Price.ToString() + " " + Date.ToString();
        }
    }
}
