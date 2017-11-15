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
    public class Client
    {
        private HttpClient client;
        private string serverRootUri;

        public Client(string _serverRootUri)
        {
            client = new HttpClient();
            serverRootUri = _serverRootUri;
        }

        public async Task<Stream> Request<T>(Stream body = null, NameValueCollection query = null) where T : IEndpointMeta, new()
        {
            T endpoint = new T();

            var requestUri = $"{serverRootUri}/{endpoint.GetURI()}";

            if (query != null)
            {
                var queryString = String.Join("&", query.AllKeys.Select(a => a + "=" + System.Net.WebUtility.UrlEncode(query[a])));
                requestUri += queryString;
            }

            var message = new HttpRequestMessage(endpoint.GetMethod(), requestUri);

            if (body != null)
            {
                var content = new StreamContent(body);
                message.Content = content;
            }

            var result = await client.SendAsync(message);
            return await result.Content.ReadAsStreamAsync();
        }

    }
}