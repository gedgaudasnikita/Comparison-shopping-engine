using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine.Controller
{
    public static class Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Receipt ProcessReceipt(Bitmap source)
        {
            Receipt receipt = Receipt.Convert(source);
            return ProcessReceipt(receipt);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Receipt ProcessReceipt(Receipt source)
        {
            ItemManager manager = ItemManager.GetInstance();
            return source;
        }
    }
}
