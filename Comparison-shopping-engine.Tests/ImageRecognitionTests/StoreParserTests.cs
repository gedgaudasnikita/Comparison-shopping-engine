using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine.Tests
{
    [TestClass()]
    public class StoreParserTests
    {
        [TestMethod()]
        public void ParseTest_parsesStoreName()
        {
            string receipt = @"MAXXMX";

            StoreParser sp = new StoreParser();

            string store = sp.Parse(receipt);
            Assert.AreEqual("MAXIMA", store);
        }

        [TestMethod()]
        public void ParseTest_doesntProvideResultIfUnparseable()
        {
            string receipt = @"vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv";

            StoreParser sp = new StoreParser();

            string store = sp.Parse(receipt);
            Assert.AreEqual("", store);
        }
    }
}