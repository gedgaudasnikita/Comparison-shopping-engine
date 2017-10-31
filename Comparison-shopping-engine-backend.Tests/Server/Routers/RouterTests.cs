using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Collections.Specialized;
using System.Net.Http;

namespace Comparison_shopping_engine_backend
{
    [TestClass]
    public class RouterTests
    {
        private class TestEndpoint : IEndpoint
        {
            public Callback Handler { get; set; }

            public Callback GetHandler()
            {
                return Handler;
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

        [TestMethod]
        public void GetCallback_getsCorrectCallback()
        {
            TestEndpoint endpoint = new TestEndpoint();
            Callback cb = (Stream body, NameValueCollection queryString) => { return "testString"; };
            endpoint.Handler = cb;

            Router test = new Router(new List<IEndpoint> { endpoint });
            
            Callback result = test.GetCallback("test", HttpMethod.Get);

            Assert.AreEqual(cb, result);
        }
    }
}
