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
        private Dictionary<Tuple<string, HttpMethod>, Callback> URIMap;
        
        public Router (List<IEndpoint> endpoints)
        {
            URIMap = new Dictionary<Tuple<string, HttpMethod>, Callback>();
            endpoints.ForEach((endpoint) => {
                URIMap.Add(new Tuple<string, HttpMethod>(endpoint.GetURI(), endpoint.GetMethod()), endpoint.GetHandler());
            });
        }

        /// <summary>
        /// Returns a <see cref="Callback"/> that represents the functionality fot the given endpoint.
        /// Throws a KeyNotFoundException in case the endpoint is not stored.
        /// </summary>
        /// <param name="endpoint">A <see cref="string"/> that identifies the endpoint URI</param>
        /// <param name="method">A <see cref="HttpMethod"/> that identifies the method used</param>
        /// <returns>A respective <see cref="Callback"/></returns>
        public Callback GetCallback(string endpoint, HttpMethod method)
        {
            return URIMap[new Tuple<string, HttpMethod>(endpoint, method)];
        }
    }
}
