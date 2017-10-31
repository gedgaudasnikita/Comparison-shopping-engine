using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public string GetURI()
        {
            return "ProcessReceipt";
        }

        private string ProcessReceipt(Stream input, NameValueCollection inputQuery)
        {
            Receipt given = serializer.Deserialize<Receipt>(new StreamReader(input).ReadToEnd());

            List<Item> result = Controller.ProcessReceipt(given);

            return serializer.Serialize(result);
        }
    }
}
