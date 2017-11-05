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
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to return the list of the best matching item names stored in the normalisation engine given the
    /// input name.
    /// </summary>
    public class GetSuggestionsBackendEndpoint : GetSuggestionsEndpoint, IBackendEndpoint
    {
        public Stream Handle(Stream body, NameValueCollection query)
        {
            var queryParameters = GetRequestParameters(query);

            var result = NormalizationEngine.GetInstance().GetClosestList(queryParameters.Input, queryParameters.Limit);

            return GetResponseBody(result);
        }
    }
}
