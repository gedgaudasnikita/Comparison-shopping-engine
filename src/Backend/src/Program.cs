using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    static class Program
    {
        private static IServer server;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (!InitEntities())
            {
                CloseOnUserEntry("Initialization failed.");
                return;
            }

            server.Start();
            DBTest(); //Used for initial DB creation and testing, delete later
            CloseOnUserEntry();
        }

        static void CloseOnUserEntry(string addInfo = "")
        {
            Console.WriteLine($"{addInfo}Press any key to kill the process");
            Console.ReadKey();
            DestroyEntities();
        }

        static void DestroyEntities()
        {
            if (server != null)
                server.Dispose();
        }

        static bool InitEntities()
        {
            bool initialized = false;

            try
            {
                //Core initialization
                ParseableReceipt.ItemListParser = new ItemListParser();
                ParseableReceipt.DateParser = new DateParser();
                ParseableReceipt.StoreParser = new StoreParser();

                ItemManager.GetInstance().LoadAll();
                NormalizationEngine.GetInstance().LoadAll();

                //Server initialization
                var endpoints = new List<IBackendEndpoint>()
                {
                    new GetSuggestionsBackendEndpoint(),
                    new ProcessImageBackendEndpoint(),
                    new ProcessReceiptBackendEndpoint(),
                    new SaveReceiptBackendEndpoint()
                };

                server = new HttpServer(new Router(endpoints));

                initialized = true;
            }
            catch (JsonReaderException)
            {
                Console.WriteLine("Deserialization failed.");
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Configuration format corrupt.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return initialized;
        }

        static void DBTest()
        {
            using (var db = new ItemsContext())
            {
                //Add old items to DB
                DBItem item1 = new DBItem
                {
                    Name = "KELMES pienas, 2,5% rieb.",
                    Store = "IKI",
                    Price = 10,
                    Date = DateTime.Now.Date
                };

                DBItem item2 = new DBItem
                {
                    Name = "Malta kava Paulig Extra“",
                    Store = "MAXIMA",
                    Price = 899,
                    Date = DateTime.Now.Date
                };

                DBItem item3 = new DBItem
                {
                    Name = "Ledai Maxima Favorit",
                    Store = "MAXIMA",
                    Price = 129,
                    Date = DateTime.Now.Date
                };

                db.Items.Add(item1);
                db.Items.Add(item2);
                db.Items.Add(item3);
                db.SaveChanges();

                //Display all items
                var query = from i in db.Items
                            select i;
                foreach (DBItem item in query)
                    System.Console.Out.WriteLine(item.Name + " " + item.Store + " " + item.Price.ToString() + " " + item.Date.ToString());
            }
        }
    }
}
