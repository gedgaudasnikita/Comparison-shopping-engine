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
    class ProcessImageBackendEndpoint : ProcessImageEndpoint, IBackendEndpoint
    {
        public Stream Handle(Stream body, NameValueCollection query)
        {
            var image = GetRequestBody(body);

            var result = Controller.ProcessImage(image);

            return GetResponseBody(result);
        }
    }
}
