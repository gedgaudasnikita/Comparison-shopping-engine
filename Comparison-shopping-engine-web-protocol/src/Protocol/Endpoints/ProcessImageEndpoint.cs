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

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to parse photographed receipt into a Receipt object, and return it to the end user
    /// </summary>
    public class ProcessImageEndpoint : IEndpoint<string, object, Receipt>
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public string GetRequestBody(Stream bodyObject)
        {
            return new StreamReader(bodyObject).ReadToEnd();
        }

        public Stream GetRequestBody(string bodyObject)
        {
            var bytes = Encoding.UTF8.GetBytes(bodyObject);
            return new MemoryStream(bytes);
        }

        public NameValueCollection GetRequestParameters(object requestParameters)
        {
            return new NameValueCollection();
        }

        public object GetRequestParameters(NameValueCollection requestParameters)
        {
            return null;
        }

        public Receipt GetResponseBody(Stream responseObject)
        {
            return serializer.Deserialize<Receipt>(responseObject);
        }

        public Stream GetResponseBody(Receipt responseObject)
        {
            MemoryStream resultStream = new MemoryStream();

            serializer.Serialize(resultStream, responseObject);

            return resultStream;
        }

        public string GetURI()
        {
            return "ProcessImage";
        }
    }
}
