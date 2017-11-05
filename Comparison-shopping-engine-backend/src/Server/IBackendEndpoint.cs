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
    public delegate Stream Handler(Stream body, NameValueCollection query);
    /// <summary>
    /// Implemented by the classes that aim to encapsulate the behaviour of certain server endpoints.
    /// Provides an additional layer in handling the request, used for IO data preparation and handling.
    /// </summary>
    public interface IBackendEndpoint: IEndpointMeta
    {
        Stream Handle(Stream body, NameValueCollection query);
    }
}
