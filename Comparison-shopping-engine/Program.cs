using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comparison_shopping_engine
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            ItemManager manager = ItemManager.Init();
            manager.AddItem(new Item("Batonas", "Maxima", 5.00, "2017-09-20"));
            manager.AddItem(new Item("Batonas", "Norfa", 3.00, "2017-09-19"));
            manager.AddItem(new Item("Arbata", "Maxima", 2.99, "2017-09-20"));
            //manager.CompareAddItem(new Item("Batonas", "Maxima", 4.00, "2017-09-24"));
            //manager.CompareAddItem(new Item("Batonas", "Maxima", 13.00, "2016-10-10"));

            manager.FindCheaper(new Item("Batonas", "Maxima", 5.00, "2017-09-11")).Print();
        }
    }
}
