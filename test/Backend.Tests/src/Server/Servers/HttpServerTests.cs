using System;
using NUnit.Framework;
using Comparison_shopping_engine_backend;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Collections.Generic;
using System.Reflection;
using System.Net;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class HttpServerTests
    {
        private class StubEndpoint : IBackendEndpoint
        {
            public HttpMethod GetMethod()
            {
                return HttpMethod.Get;
            }

            public string GetURI()
            {
                return "test";
            }

            public Stream Handle(Stream body, NameValueCollection query)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void ConstructorTest_setsCorrectValues()
        {
            var expectedRouter = new Router(new List<IBackendEndpoint> { new StubEndpoint() });
            using (HttpServer testServer = new HttpServer(expectedRouter))
            {

                var prop = typeof(HttpServer).GetField("router", BindingFlags.NonPublic | BindingFlags.Instance);
                var router = (Router)prop.GetValue(testServer);

                testServer.Start();

                Assert.AreEqual(expectedRouter, router);
            }
        }
    }
}
