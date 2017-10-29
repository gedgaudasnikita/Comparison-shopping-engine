using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Comparison_shopping_engine_backend
{
    class ProcessReceiptEndpoint : IEndpoint
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Callback GetHandler()
        {
            return ProcessReceipt;
        }

        public string GetURI()
        {
            return "ProcessReceipt";
        }

        private string ProcessReceipt(Stream input)
        {
            Receipt given = serializer.Deserialize<Receipt>(new StreamReader(input).ReadToEnd());

            List<Item> result = Controller.ProcessReceipt(given);

            return serializer.Serialize(result);
        }
    }
}
