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
            //Testing the DB
            DBManager.SetupDB();
            DBManager.PrintDB();
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
    }
}
