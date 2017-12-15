using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    public class Router: IRouter
    {
        private Dictionary<Tuple<string, HttpMethod>, Handler> URIMap;
        
        public Router (List<IBackendEndpoint> endpoints)
        {
            URIMap = new Dictionary<Tuple<string, HttpMethod>, Handler>();
            endpoints.ForEach((endpoint) => {
                URIMap.Add(new Tuple<string, HttpMethod>(endpoint.GetURI(), endpoint.GetMethod()), endpoint.Handle);
            });
        }

        /// <summary>
        /// Returns a <see cref="Handler"/> that represents the functionality fot the given endpoint.
        /// Throws a KeyNotFoundException in case the endpoint is not stored.
        /// </summary>
        /// <param name="endpoint">A <see cref="string"/> that identifies the endpoint URI</param>
        /// <param name="method">A <see cref="HttpMethod"/> that identifies the method used</param>
        /// <returns>A respective <see cref="Handler"/></returns>
        public Handler GetHandler(string endpoint, HttpMethod method)
        {
            return URIMap[new Tuple<string, HttpMethod>(endpoint, method)];
        }
    }
}
