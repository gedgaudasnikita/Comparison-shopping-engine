using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// Implemented by the classes that aim to encapsulate such qualities of the web protocol endpoint as
    /// the URI and the HTTP method
    /// </summary>
    public interface IEndpointMeta
    {
        /// <summary>
        /// Returns the relative (to the root) URI for the endpoint being encapsulated
        /// </summary>
        /// <returns>A <see cref="string"/>, containing the relative URI</returns>
        string GetURI();

        /// <summary>
        /// Returns the method to be called upon the endpoint
        /// </summary>
        /// <returns>A <see cref="HttpMethod"/> for the endpoint</returns>
        HttpMethod GetMethod();
    }
}
