using Comparison_shopping_engine_notification_protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace Comparison_shopping_engine_admin
{
    /// <summary>
    /// This class is responsible for sending the notification requests to FCM
    /// </summary>
    public class NotificationSender
    {
        /// <summary>
        /// Sends a <see cref="HttpWebRequest"/> to the FCM builder with the given data.
        /// </summary>
        /// <param name="notification">The <see cref="NotificationData"/> object to be sent</param>
        public void Send(NotificationData notification)
        {
            NotificationWebRequestBuilder.GetNotificationWebRequest(
                ConfigurationManager.AppSettings["notificationTopic"], 
                new StreamReader(notification.Serialize()).ReadToEnd()
            ).GetResponse();
        }
    }
}
