using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestClass()]
    public class GetSuggestionsEndpointTests
    {
        [TestMethod()]
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

        [TestMethod()]
        public void GetResponseBodyTest_parsesCorrectly()
        {
            var endpoint = new GetSuggestionsEndpoint();
            var response = new List<string>{ "string1", "string2" };

            var result = endpoint.GetResponseBody(endpoint.GetResponseBody(response));

            Assert.AreEqual(true, result.SequenceEqual(response));
        }
    }
}