using NUnit.Framework;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class NormalizationEngineTests
    {
        [Test]
        public void GetClosestTest_getsClosest()
        {
            NormalizationEngine.AddName("very unlikely to get matched with");
            NormalizationEngine.AddName("pretty likely");

            var result = NormalizationEngine.GetClosest("pretty Lily");

            Assert.AreEqual("pretty likely", result);
        }

        [Test]
        public void GetClosestTest_returnsEmptyStringIfNoMatch()
        {
            NormalizationEngine.ClearList();

            var result = NormalizationEngine.GetClosest("pretty Lily");

            Assert.AreEqual("", result);
        }

        [Test]
        public void GetClosestListTest_getsClosest()
        {
            NormalizationEngine.AddName("very unlikely to get matched with");
            NormalizationEngine.AddName("pretty likely");

            var result = NormalizationEngine.GetClosestList("pretty Lily", 2);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("pretty likely", result.ElementAt(0));
            Assert.AreEqual("very unlikely to get matched with", result.ElementAt(1));
        }

        [Test]
        public void GetClosestListTest_returnsEmptyListIfNoMatch()
        {
            NormalizationEngine.ClearList();

            var result = NormalizationEngine.GetClosestList("pretty Lily", 2);

            Assert.AreEqual(0, result.Count());
        }
    }
}