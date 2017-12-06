using NUnit.Framework;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class StoreParserTests
    {
        [Test]
        public void ParseTest_parsesStoreName()
        {
            string receipt = @"MAXXMX";

            StoreParser sp = new StoreParser();

            string store = sp.Parse(receipt);
            Assert.AreEqual("MAXIMA", store);
        }

        [Test]
        public void ParseTest_doesntProvideResultIfUnparseable()
        {
            string receipt = @"vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv";

            StoreParser sp = new StoreParser();

            string store = sp.Parse(receipt);
            Assert.AreEqual("", store);
        }
    }
}