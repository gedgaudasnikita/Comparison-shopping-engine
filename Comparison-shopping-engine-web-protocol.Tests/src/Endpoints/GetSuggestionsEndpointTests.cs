using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison_shopping_engine_web_protocol;

namespace Comparison_shopping_engine_web_protocol.Tests
{
    [TestFixture]
    public class GetSuggestionsEndpointTests
    {
        [Test]
        public void GetRequestParametersTest_parsesCorrectly()
        {
            var endpoint = new GetSuggestionsEndpoint();
            var parameters = new GetSuggestionsParameters
            {
                Input = "input",
                Limit = 10
            };

            var result = endpoint.GetRequestParameters(endpoint.GetRequestParameters(parameters));

            Assert.AreEqual(parameters, result);
        }

        [Test]
        public void GetResponseBodyTest_parsesCorrectly()
        {
            var endpoint = new GetSuggestionsEndpoint();
            var response = new List<string>{ "string1", "string2" };

            var result = endpoint.GetResponseBody(endpoint.GetResponseBody(response));

            Assert.AreEqual(true, result.SequenceEqual(response));
        }
    }
}