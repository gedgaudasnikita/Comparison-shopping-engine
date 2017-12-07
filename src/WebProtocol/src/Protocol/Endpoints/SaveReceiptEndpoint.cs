using Comparison_shopping_engine_core_entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_web_protocol
{
    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to save the given Receipt object to the database.
    /// </summary>
    public class SaveReceiptEndpoint : IEndpoint<Receipt, object, object>
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public Receipt GetRequestBody(Stream bodyObject)
        {
            return serializer.Deserialize<Receipt>(bodyObject);
        }

        public Stream GetRequestBody(Receipt bodyObject)
        {
            MemoryStream resultStream = new MemoryStream();

            serializer.Serialize(resultStream, bodyObject);

            return resultStream;
        }

        public NameValueCollection GetRequestParameters(object requestParameters)
        {
            return new NameValueCollection();
        }

        public object GetRequestParameters(NameValueCollection requestParameters)
        {
            return null;
        }

        public object GetResponseBody(Stream responseObject)
        {
            return null;
        }

        public Stream GetResponseBody(object responseObject)
        {
            return null;
        }

        public string GetURI()
        {
            return "SaveReceipt";
        }
    }
}
