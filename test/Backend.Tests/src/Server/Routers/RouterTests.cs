using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;
using System.Net.Http;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class RouterTests
    {
        private class TestEndpoint : IBackendEndpoint
        {
            public bool testFlag = false;

            public Stream Handle(Stream body, NameValueCollection query)
            {
                testFlag = true;
                return null;
            }

            public HttpMethod GetMethod()
            {
                return HttpMethod.Get;
            }

            public string GetURI()
            {
                return "test";
            }
        }

        [Test]
        public void GetCallbackTest_getsCorrectCallback()
        {
            TestEndpoint endpoint = new TestEndpoint();

            Router test = new Router(new List<IBackendEndpoint> { endpoint });
            
            test.GetHandler("test", HttpMethod.Get)(null, null);

            Assert.AreEqual(true, endpoint.testFlag);
        }
    }
}
