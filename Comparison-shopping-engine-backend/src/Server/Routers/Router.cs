using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend
{
    public class Router: IRouter
    {
        private Dictionary<string, Callback> URIMap;
        
        public Router (List<IEndpoint> endpoints)
        {
            URIMap = new Dictionary<string, Callback>();
            endpoints.ForEach((endpoint) => {
                URIMap.Add(endpoint.GetURI(), endpoint.GetHandler());
            });
        }

        /// <summary>
        /// Returns a <see cref="Callback"/> that represents the functionality fot the given endpoint.
        /// Throws a KeyNotFoundException in case the endpoint is not stored.
        /// </summary>
        /// <param name="endpoint">A <see cref="string"/> that identifies the endpoint URI</param>
        /// <returns>A respective <see cref="Callback"/></returns>
        public Callback GetCallback(string endpoint)
        {
            return URIMap[endpoint];
        }
    }
}
