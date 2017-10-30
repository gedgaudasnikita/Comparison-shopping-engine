using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to return the list of the best matching item names stored in the normalisation engine given the
    /// input name.
    /// </summary>
    public class GetSuggestionsEndpoint : IEndpoint
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Callback GetHandler()
        {
            return GetSuggestions;
        }

        public HttpMethod GetMethod()
        {
            return HttpMethod.Get;
        }

        public string GetURI()
        {
            return "GetSuggestions";
        }

        private string GetSuggestions(Stream body, NameValueCollection inputQuery)
        {
            var name = inputQuery["input"];

            if (name == null || !Int32.TryParse(inputQuery["limit"], out int limit))
            {
                throw new ArgumentException();
            }

            var result = NormalizationEngine.GetInstance().GetClosestList(name, limit);
            
            return serializer.Serialize(result);
        }
    }
}
