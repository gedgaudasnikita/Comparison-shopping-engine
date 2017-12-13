using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Comparison_shopping_engine_backend
{
    class ItemsContext : DbContext
    {
        public DbSet<DBItem> Items { get; set; }
        public DbSet<DBItemHistory> ItemHistories { get; set; }
    }
}
