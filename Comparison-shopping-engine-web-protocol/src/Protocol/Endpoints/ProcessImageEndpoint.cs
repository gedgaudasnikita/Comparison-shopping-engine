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
    public class ProcessImageEndpoint : IEndpoint<Bitmap, object, Receipt>
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public HttpMethod GetMethod()
        {
            return HttpMethod.Post;
        }

        public Bitmap GetRequestBody(Stream bodyObject)
        {
            return new Bitmap(bodyObject);
        }

        public Stream GetRequestBody(Bitmap bodyObject)
        {
            var stream = new MemoryStream();
            bodyObject.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream;
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
