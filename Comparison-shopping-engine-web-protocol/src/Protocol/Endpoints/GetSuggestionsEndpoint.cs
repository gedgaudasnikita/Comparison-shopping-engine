using Comparison_shopping_engine_core_entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_web_protocol
{
    public struct GetSuggestionsParameters
    {
        public string Input { get; set; }
        public int Limit { get; set; }
    }

    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to return the list of the best matching item names stored in the normalisation engine given the
    /// input name.
    /// </summary>
    public class GetSuggestionsEndpoint: IEndpoint<object, GetSuggestionsParameters, List<string>>
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public HttpMethod GetMethod()
        {
            return HttpMethod.Get;
        }

        public object GetRequestBody(Stream bodyObject)
        {
            return null;
        }

        public Stream GetRequestBody(object bodyObject)
        {
            return null;
        }

        public NameValueCollection GetRequestParameters(GetSuggestionsParameters requestParameters)
        {
            return new NameValueCollection
            {
                { "Input", requestParameters.Input },
                { "Limit", requestParameters.Limit.ToString() }
            };
        }

        public GetSuggestionsParameters GetRequestParameters(NameValueCollection requestParameters)
        {
            if (requestParameters["Input"] == null || !Int32.TryParse(requestParameters["Limit"], out int limit))
            {
                throw new ArgumentException();
            }

            return new GetSuggestionsParameters()
            {
                Input = requestParameters["Input"],
                Limit = limit
            };
        }

        public List<string> GetResponseBody(Stream responseObject)
        {
            return serializer.Deserialize<List<string>>(responseObject);
        }

        public Stream GetResponseBody(List<string> responseObject)
        {
            MemoryStream resultStream = new MemoryStream();

            serializer.Serialize(resultStream, responseObject);

            return resultStream;
        }

        public string GetURI()
        {
            return "GetSuggestions";
        }
    }
}
