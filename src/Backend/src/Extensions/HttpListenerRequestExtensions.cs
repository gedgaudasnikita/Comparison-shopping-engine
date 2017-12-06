using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// The class, containing all the custom extension methods for the <see cref="HttpListenerRequest"/> type
    /// </summary>
    public static class HttpListenerRequestExtensions
    {
        /// <summary>
        /// Returns the first segment of the URI, used as an endpoint identifier
        /// </summary>
        /// <param name="rqs">The <see cref="HttpListenerRequest"/> for the method to be called on.</param>
        /// <returns>A <see cref="string"/>, identifying the endpoint</returns>
        public static string GetEndpoint(this HttpListenerRequest rqs)
        {
            return rqs.Url.Segments[1].Replace("/", "");
        }
    }
}
