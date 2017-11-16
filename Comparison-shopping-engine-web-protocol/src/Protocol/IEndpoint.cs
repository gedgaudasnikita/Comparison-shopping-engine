using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_web_protocol
{
    /// <summary>
    /// Implemented by the classes that aim to encapsulate such qualities of the web protocol endpoint as
    /// the URI,the HTTP method, the parameter types, the response types and the protocol format
    /// </summary>
    public interface IEndpoint<RequestBody, RequestParameters, ResponseBody>: IEndpointMeta
    {
        /// <summary>
        /// Performs both sides of an endpoint-specific conversion of body parameters. Used to encapsulate 
        /// the types of body parameters used for a specific request in a protocol and the format of their
        /// serialization
        /// </summary>
        RequestBody GetRequestBody(Stream bodyObject);
        Stream GetRequestBody(RequestBody bodyObject);

        /// <summary>
        /// Performs both sides of an endpoint-specific conversion of query parameters. Used to encapsulate 
        /// the types of query parameters used for a specific request in a protocol and the format of their
        /// serialization
        /// </summary>
        NameValueCollection GetRequestParameters(RequestParameters requestParameters);
        RequestParameters GetRequestParameters(NameValueCollection requestParameters);

        /// <summary>
        /// Performs both sides of an endpoint-specific conversion of response body. Used to encapsulate 
        /// the type of response body used for a specific response in a protocol and the format of its
        /// serialization
        /// </summary>
        ResponseBody GetResponseBody(Stream responseObject);
        Stream GetResponseBody(ResponseBody responseObject);
    }
}
