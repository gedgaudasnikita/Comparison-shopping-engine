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
    /// to save the given Receipt object to the database.
    /// </summary>
    class SaveReceiptBackendEndpoint : SaveReceiptEndpoint, IBackendEndpoint
    {    
        public Stream Handle(Stream body, NameValueCollection query)
        {
            var receipt = GetRequestBody(body);

            Controller.SaveReceipt(receipt);

            return GetResponseBody(new object());
        }
    }
}
