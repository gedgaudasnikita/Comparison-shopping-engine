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

        public Callback GetCallback(string endpoint)
        {
            return URIMap[endpoint];
        }
    }
}
