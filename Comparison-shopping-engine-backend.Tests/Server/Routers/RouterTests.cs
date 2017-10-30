using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Comparison_shopping_engine_backend
{
    [TestClass]
    public class RouterTests
    {
        private class TestEndpoint : IEndpoint
        {
            public Callback handler { get; set; }

            public Callback GetHandler()
            {
                return handler;
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
            Callback cb = (Stream input) => { return "testString"; };
            endpoint.handler = cb;

            Router test = new Router(new List<IEndpoint> { endpoint });
            
            Callback result = test.GetCallback("test");

            Assert.AreEqual(cb, result);
        }
    }
}
