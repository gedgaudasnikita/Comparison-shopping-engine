using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Comparison_shopping_engine_backend
{
    class DBItem
    {
        [Key]
        public string Name { get; set; }
        public string Store { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public virtual List<DBItemHistory> ItemHistories { get; set; }
    }
}
