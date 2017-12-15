using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_admin
{
    /// <summary>
    /// The class, responsible for creating all the necessary data to the <see cref="HttpWebRequest"/>
    /// needed to perform a FCM notification push
    /// </summary>
    public static class NotificationWebRequestBuilder
    {
        /// <summary>
        /// Create a <see cref="HttpWebRequest"/> object filled with necessary metadata
        /// </summary>
        /// <param name="topic">The topic to push the notification to</param>
        /// <param name="data">The data to be sent</param>
        /// <returns>The created and filled request object</returns>
        public static HttpWebRequest GetNotificationWebRequest(string topic, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAQIXAYOU:APA91bGMzMnP81PqY0LfFZcn7tGdLJec09DzDHTbV_3Ofi6qmQytQpOn9NsK9nn_r35oev7QL1cV1hDPQDrtxa1W4qV0_bJ7KvZA_uEA1nTHAEYLfrVj9as1FAcs63aUGN7MXTa0PWuY");
            httpWebRequest.Headers.Add(string.Format("Sender: id={0}", "277121884389"));
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = $@"{{
                    ""to"": ""/topics/{topic}"",
                    ""priority"": ""high"",
                    ""data"": {data}
                }}";
                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            return httpWebRequest;
        }
    }
}
