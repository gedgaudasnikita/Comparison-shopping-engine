using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_web_protocol.Tests
{
    [TestFixture]
    public class ProcessReceiptEndpointTests
    {
        [Test]
        public void GetRequestBodyTest_parsesCorrectly()
        {
            var endpoint = new ProcessReceiptEndpoint();
            var receipt = new Receipt()
            {
                Store = "test"
            };

            var result = endpoint.GetRequestBody(endpoint.GetRequestBody(receipt));
            
            Assert.AreEqual(true, result.Equals(receipt));
        }

        [Test]
        public void GetResponseBodyTest_parsesCorrectly()
        {
            var endpoint = new ProcessReceiptEndpoint();
            var list = new List<Item>()
            {
                new Item()
                {
                    Name = "test"
                }
            };

            var result = endpoint.GetResponseBody(endpoint.GetResponseBody(list));

            Assert.AreEqual(true, result.SequenceEqual(list));
        }
    }
}