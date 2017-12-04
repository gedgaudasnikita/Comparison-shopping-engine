using System;
using NUnit.Framework;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void RemoveNonDigits_removesDigits()
        {
            string input = "w1a2d123asd";

            string result = input.RemoveNonDigits();

            Assert.AreEqual("12123", result);
        }

        [Test]
        public void GetDistance_calculatesDistanceCorrectly()
        {
            string input1 = "hey";
            string input2 = "heyy";

            int result = input1.GetDistance(input2);

            Assert.AreEqual(1, result);
        }
    }
}
