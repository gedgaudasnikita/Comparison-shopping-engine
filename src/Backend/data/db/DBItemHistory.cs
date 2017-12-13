using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comparison_shopping_engine_backend
{
    class DBItemHistory
    {
        [Key]
        [Column(Order = 1)]
        public string Store { get; set; }
        [Key]
        [Column(Order = 2)]
        public int Price { get; set; }
        [Key]
        [Column(Order = 3)]
        public DateTime Date { get; set; }

        [ForeignKey("Item")]
        public string Name { get; set; }
        public virtual DBItem Item { get; set; }
    }
}
