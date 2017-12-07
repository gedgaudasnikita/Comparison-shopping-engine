using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Comparison_shopping_engine_web_protocol;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// An HttpClient wrapper class adapted to the IEndpoint concept
    /// </summary>
    public class Client
    {
        private HttpClient client;
        private string serverRootUri;

        public Client(string _serverRootUri)
        {
            client = new HttpClient();
            serverRootUri = _serverRootUri;
        }

        /// <summary>
        /// Performs an HTTP request to the endpoint, specified by the <typeparamref name="T"/> type with the given
        /// data.
        /// </summary>
        /// <typeparam name="T">The class that inherits IEndpointMeta and encapsulates the information for the necessary endpoint</typeparam>
        /// <param name="body">The <see cref="Stream"/> containing the body data of the request, optional</param>
        /// <param name="query">The <see cref="NameValueCollection"/> containing the parameter query of the request, optional</param>
        /// <returns>A <see cref="Stream"/> containing the response body information</returns>
        public async Task<Stream> Request<T>(Stream body = null, NameValueCollection query = null) where T : IEndpointMeta, new()
        {
            T endpoint = new T();

            var requestUri = $"{serverRootUri}/{endpoint.GetURI()}";

            if (query != null)
            {
                var queryString = String.Join("&", query.AllKeys.Select(a => a + "=" + System.Net.WebUtility.UrlEncode(query[a])));
                requestUri += $"?{queryString}";
            }

            var message = new HttpRequestMessage(endpoint.GetMethod(), requestUri);

            if (body != null)
            {
                var content = new StreamContent(body);
                message.Content = content;
            }

            HttpResponseMessage result = new HttpResponseMessage();

            result = await client.SendAsync(message);

            Stream resultStream = new MemoryStream();
            if (result.IsSuccessStatusCode)
            {
                resultStream = await result.Content.ReadAsStreamAsync();
            }
           
            return resultStream;
        }

    }
}