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
            //Used for initial DB creation and testing, delete after
            //SetUpDB();
            TestDB();
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

        static void SetUpDB()
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
            }
        }

        static void TestDB()
        {
        }
    }
}
