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
    /// to match the given Receipt object against the stored Items and return the cheapest alternatives
    /// </summary>
    public class ProcessReceiptEndpoint : IEndpoint<Receipt, Object, List<Item>>
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public string GetURI()
        {
            return "ProcessReceipt";
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

        public List<Item> GetResponseBody(Stream responseObject)
        {
            return serializer.Deserialize<List<Item>>(responseObject);
        }

        public Stream GetResponseBody(List<Item> responseObject)
        {
            MemoryStream resultStream = new MemoryStream();

            serializer.Serialize(resultStream, responseObject);

            return resultStream;
        }
    }
}
