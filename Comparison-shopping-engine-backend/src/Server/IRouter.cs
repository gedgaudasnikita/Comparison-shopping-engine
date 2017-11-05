using System.Net;
using System.Net.Http;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// The classes, implementing this interface are used to store all the accessible endpoints and provides 
    /// a quick way to handle incoming requests, in particular, recognize the respective functionality.
    /// </summary>
    public interface IRouter
    {
        /// <summary>
        /// Returns a <see cref="Callback"/> that represents the functionality fot the given endpoint.
        /// </summary>
        /// <param name="endpoint">A <see cref="string"/> that identifies the endpoint URI</param>
        /// <param name="method">A <see cref="HttpMethod"/> that identifies the method used</param>
        /// <returns>A respective <see cref="Handler"/></returns>
        Handler GetHandler(string endpoint, HttpMethod method);
    }
}