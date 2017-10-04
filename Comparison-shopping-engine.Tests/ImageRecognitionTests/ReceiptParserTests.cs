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
    public class ParserTests
    {
        [TestMethod()]
        public void ParseStoreTest_parsesStore()
        {
            string receipt = @"!x!";

            string store = Parser.ParseStore(receipt);
            Assert.AreEqual("IKI", store);
        }
    }
}