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
            InitEntities();
            server.Start();
            Console.WriteLine("Press any key to kill the process");
            Console.ReadKey();
            server.Dispose();
        }

        static void InitEntities()
        {
            //Core initialization
            ParseableReceipt.ItemListParser = new ItemListParser();
            ParseableReceipt.DateParser = new DateParser();
            ParseableReceipt.StoreParser = new StoreParser();
            ItemManager.GetInstance().LoadAll();
            NormalizationEngine.GetInstance().LoadAll();

            //Server initialization
            var endpoints = new List<IEndpoint>() { new GetSuggestionsEndpoint(),
                                                    new ProcessImageEndpoint(),
                                                    new ProcessReceiptEndpoint(),
                                                    new SaveReceiptEndpoint()}; 
            server = new HttpServer(ConfigurationManager.AppSettings["serverURL"], new Router(endpoints));
        }
    }
}
