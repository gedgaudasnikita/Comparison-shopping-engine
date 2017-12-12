using System;
using NUnit.Framework;
using Comparison_shopping_engine_notification_protocol;

namespace Comparison_shopping_engine_notification_protocol.Tests
{
    [TestFixture]
    public class NotificationDataTests
    {
        [Test]
        public void SerializeTest_convertsProperly()
        {
            NotificationData source = new NotificationData();

            source.MapItemToText.Add("hey", "hey");

            NotificationData result = new NotificationData(source.Serialize());

            Assert.AreEqual(1, result.MapItemToText.Count);
        }
    }
}
