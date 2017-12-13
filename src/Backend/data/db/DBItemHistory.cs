using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    class DBItemHistory
    {
        public string Store { get; set; }

        public int Price { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public virtual DBItem Item { get; set; }
    }
}
