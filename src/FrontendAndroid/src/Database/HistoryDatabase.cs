using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_frontend_android
{
    public class HistoryDatabase
    {
        private SQLiteAsyncConnection database;

        public HistoryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ItemHistory>().Wait();
        }

        public Task<int> AddItem(ItemHistory item)
        {
            return database.ExecuteAsync("INSERT OR REPLACE INTO ItemHistory (ItemName, Count) VALUES (?, ifnull((SELECT Count FROM ItemHistory WHERE ItemName = ?), 0) + 1)", item.ItemName, item.ItemName);
        }

        public Task<List<ItemHistory>> GetTopItems(int amount)
        {
            return database.Table<ItemHistory>().OrderBy(item => item.Count).ToListAsync();
        }
    }
}