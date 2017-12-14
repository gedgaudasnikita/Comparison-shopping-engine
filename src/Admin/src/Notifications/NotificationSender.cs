using Comparison_shopping_engine_notification_protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Comparison_shopping_engine_admin
{
    public static class NotificationSender
    {
        public static void Send(string itemName, string notificationText)
        {
            var notification = new NotificationData()
            {
                MapItemToText = new Dictionary<string, string>() { { itemName, notificationText } }
            };

            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAQIXAYOU:APA91bGMzMnP81PqY0LfFZcn7tGdLJec09DzDHTbV_3Ofi6qmQytQpOn9NsK9nn_r35oev7QL1cV1hDPQDrtxa1W4qV0_bJ7KvZA_uEA1nTHAEYLfrVj9as1FAcs63aUGN7MXTa0PWuY");
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", "277121884389"));
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = $@"{{
                    ""to"": ""/topics/all"",
                    ""notification"": {{
                        ""text"": {new StreamReader(notification.Serialize()).ReadToEnd()}
                    }}
                }}";
                System.Console.WriteLine(strNJson);
                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            Console.WriteLine(result);
        }
    }
}
