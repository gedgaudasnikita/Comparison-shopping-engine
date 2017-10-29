using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// The class, containing all the custom extension methods for the <see cref="HttpListenerContext"/> type
    /// </summary>
    public static class HttpListenerContextExtensions
    {
        /// <summary>
        /// Returns the first segment of the URI, used as an endpoint identifier
        /// </summary>
        /// <param name="ctx">The <see cref="HttpListenerContext"/> for the method to be called on.</param>
        /// <returns>A <see cref="string"/>, identifying the endpoint</returns>
        public static string GetEndpoint(this HttpListenerContext ctx)
        {
            return ctx.Request.Url.Segments[1].Replace("/", "");
        }
    }
}
