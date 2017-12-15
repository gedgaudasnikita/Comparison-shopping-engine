using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison_shopping_engine_core_entities;
using System.Data;

namespace Comparison_shopping_engine_admin
{
    public class DbClient
    {
        public bool Active = false;
        private SqlConnection con;

        public DbClient()
        {
            con = new SqlConnection(@"Data Source=LENOVO-PC\SQLEXPRESS;Initial Catalog=Comparison_shopping_engine_backend.ItemsContext;Integrated Security=True;");
            Active = true;
        }

        public SqlDataAdapter GetAllItems()
        {
             return new SqlDataAdapter("Select * From DBItems", con);
        }

        public void UpdateItem(Item item)
        {
            var adap = new SqlDataAdapter
            {
                UpdateCommand = new SqlCommand("UPDATE DBItems SET Store = @store AND Date = @date AND Price = @price WHERE Name = @name", con)
            };
            adap.UpdateCommand.Parameters.AddWithValue("@store", item.Store);
            adap.UpdateCommand.Parameters.AddWithValue("@date", item.Date);
            adap.UpdateCommand.Parameters.AddWithValue("@price", item.Price);
            adap.UpdateCommand.Parameters.AddWithValue("@name", item.Name);
            adap.Update(new DataSet());
        }
    }
}
