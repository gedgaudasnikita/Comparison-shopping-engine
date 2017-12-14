using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Firebase.Iid;
using Android.Util;
using Firebase.Messaging;

namespace Comparison_shopping_engine_frontend_android
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class NotificationIdService: FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            FirebaseMessaging.Instance.SubscribeToTopic(Configuration.notificationTopic);
        }
    }
}