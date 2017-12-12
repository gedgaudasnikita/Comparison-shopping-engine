using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using Comparison_shopping_engine_notification_protocol;
using System.IO;
using System.Collections.Generic;

namespace Comparison_shopping_engine_frontend_android
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class NotificationMessagingService : FirebaseMessagingService
    {
        public override async void OnMessageReceived(RemoteMessage message)
        {
            string body = message.GetNotification().Body;

            var parsedData = new NotificationData(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body ?? "")));

            if (parsedData.MapItemToText != null)
            {
                var topItemsLocal = await AppData.Database.GetTopItems(Configuration.topItems);

                foreach (ItemHistory item in topItemsLocal)
                {
                    if (parsedData.MapItemToText.TryGetValue(item.ItemName, out string text))
                    {
                        UiHelpers.ShowNotification(this, text);
                        return;
                    }
                }
            }
        }
    }
}