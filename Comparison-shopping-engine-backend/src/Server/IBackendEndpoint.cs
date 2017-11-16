using Comparison_shopping_engine_web_protocol;
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
    /// Matches the signature of any Handle method by IBackendEndpoint
    /// </summary>
    /// <param name="body">An body stream of the request, contains the data passed in the body</param>
    /// <param name="query">A <see cref="NameValueCollection"/> that represents an input query of the request</param>
    /// <returns>A <see cref="Stream"/> to be returned to the end user as the response body</returns>
    public delegate Stream Handler(Stream body, NameValueCollection query);

    /// <summary>
    /// Implemented by the classes that aim to encapsulate the behaviour of certain server endpoints.
    /// Provides an additional layer in handling the request, used for IO data preparation and handling.
    /// When implementing this interface, it is highly recommended to inherit from some 
    /// <see cref="IEndpoint{RequestBody, RequestParameters, ResponseBody}"/> implementing class and delegate
    /// as much work as possible to the methods implemented by it.
    /// </summary>
    public interface IBackendEndpoint: IEndpointMeta
    {
        /// <summary>
        /// Prepares the data to be handled by the Controller and calls the respective method in it.
        /// </summary>
        /// <param name="body">An body stream of the request, contains the data passed in the body</param>
        /// <param name="query">A <see cref="NameValueCollection"/> that represents an input query of the request</param>
        /// <returns>A <see cref="Stream"/> to be returned to the end user as the response body</returns>
        Stream Handle(Stream body, NameValueCollection query);
    }
}
