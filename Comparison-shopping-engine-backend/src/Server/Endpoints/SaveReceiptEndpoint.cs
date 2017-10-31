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
    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to save the given Receipt object to the database.
    /// </summary>
    class SaveReceiptEndpoint : IEndpoint
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Callback GetHandler()
        {
            return SaveReceipt;
        }

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public string GetURI()
        {
            return "SaveReceipt";
        }
    
        private string SaveReceipt(Stream input, NameValueCollection inputQuery)
        {
            Receipt given = serializer.Deserialize<Receipt>(new StreamReader(input).ReadToEnd());

            Controller.SaveReceipt(given);

            return "";
        }
    }
}
