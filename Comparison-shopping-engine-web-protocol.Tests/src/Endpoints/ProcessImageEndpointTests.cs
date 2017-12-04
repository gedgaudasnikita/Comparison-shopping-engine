﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Comparison_shopping_engine_web_protocol;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestClass()]
    public class ProcessImageEndpointTests
    {
        [TestMethod()]
        public void GetRequestBodyTest_parsesCorrectly()
        {
            var endpoint = new ProcessImageEndpoint();
            var bitmap = new Bitmap("testdata/receipt.jpg");

            MemoryStream m = new MemoryStream();
            bitmap.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);

            var result = endpoint.GetRequestBody(endpoint.GetRequestBody(m));
            
            //So apparently noone ever cared to write an instance equality method for Bitmap
            //Whatever, this test is not supposed to stay in the project anyway
            Assert.AreEqual(new StreamReader(m).ReadToEnd(), new StreamReader(result).ReadToEnd());
            bitmap.Dispose();
        }

        [TestMethod()]
        public void GetResponseBodyTest_parsesCorrectly()
        {
            var endpoint = new ProcessImageEndpoint();
            var receipt = new Receipt()
            {
                Store = "test"
            };

            var result = endpoint.GetResponseBody(endpoint.GetResponseBody(receipt));

            Assert.AreEqual(true, result.Equals(receipt));
        }
    }
}