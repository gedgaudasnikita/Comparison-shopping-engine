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
    /// Prepares the data to be handled by the Controller and calls the respective method in it.
    /// </summary>
    /// <param name="body">An body stream of the request, contains the data passed in the body</param>
    /// <param name="inputQuery">A <see cref="NameValueCollection"/> that represents an optional input query from the request</param>
    /// <returns>A <see cref="string"/> to be returned to the end user as the response body</returns>
    public delegate string Callback(Stream body, NameValueCollection inputQuery);

    /// <summary>
    /// Implemented by the classes that aim to encapsulate the behaviour of certain server endpoints.
    /// Provides an additional layer in handling the request, used for IO data preparation and handling.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// Returns the relative (to the root) URI for the endpoint being encapsulated
        /// </summary>
        /// <returns>A <see cref="string"/>, containing the relative URI</returns>
        string GetURI();

        /// <summary>
        /// Returns the <see cref="Callback"/> implementing the behaviour of the endpoint
        /// </summary>
        /// <returns>An encapsulated <see cref="Callback"/></returns>
        Callback GetHandler();

        /// <summary>
        /// Returns the method to be called upon the endpoint
        /// </summary>
        /// <returns>A <see cref="HttpMethod"/> for the endpoint</returns>
        HttpMethod GetMethod();
    }
}
