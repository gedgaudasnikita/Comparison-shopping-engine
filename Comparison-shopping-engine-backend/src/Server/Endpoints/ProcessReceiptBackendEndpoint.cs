using Comparison_shopping_engine_core_entities;
using Comparison_shopping_engine_web_protocol;
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
    class ProcessReceiptBackendEndpoint : ProcessReceiptEndpoint, IBackendEndpoint
    {
        public Stream Handle(Stream body, NameValueCollection query)
        {
            var receipt = GetRequestBody(body);

            List<Item> result = Controller.ProcessReceipt(receipt);

            return GetResponseBody(result);
        }
    }
}
