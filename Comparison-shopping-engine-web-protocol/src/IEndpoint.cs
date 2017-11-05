using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// Implemented by the classes that aim to encapsulate such qualities of the web protocol endpoint as
    /// the URI,the HTTP method, the parameter types, the response types and the protocol format
    /// </summary>
    public interface IEndpoint<RequestBody, RequestParameters, ResponseBody>: IEndpointMeta
    {
        RequestBody GetRequestBody(Stream bodyObject);
        Stream GetRequestBody(RequestBody bodyObject);
        
        NameValueCollection GetRequestParameters(RequestParameters requestParameters);
        RequestParameters GetRequestParameters(NameValueCollection requestParameters);

        ResponseBody GetResponseBody(Stream responseObject);
        Stream GetResponseBody(ResponseBody responseObject);
    }
}
