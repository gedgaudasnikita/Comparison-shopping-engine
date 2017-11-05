using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestClass()]
    public class SaveReceiptEndpointTests
    {
        [TestMethod()]
        public void GetRequestBodyTest_parsesCorrectly()
        {
            var endpoint = new SaveReceiptEndpoint();
            var receipt = new Receipt()
            {
                Store = "test"
            };

            var result = endpoint.GetRequestBody(endpoint.GetRequestBody(receipt));

            Assert.AreEqual(true, result.Equals(receipt));
        }
    }
}