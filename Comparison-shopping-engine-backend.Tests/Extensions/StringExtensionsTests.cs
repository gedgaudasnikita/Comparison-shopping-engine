using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void RemoveNonDigits_removesDigits()
        {
            string input = "w1a2d123asd";

            string result = input.RemoveNonDigits();

            Assert.AreEqual("12123", result);
        }

        [TestMethod]
        public void GetDistance_calculatesDistanceCorrectly()
        {
            string input1 = "hey";
            string input2 = "heyy";

            int result = input1.GetDistance(input2);

            Assert.AreEqual(1, result);
        }
    }
}
