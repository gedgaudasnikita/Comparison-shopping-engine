using Comparison_shopping_engine_core_entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_notification_protocol
{
    public class NotificationData
    {
        private JsonSerializerStream serializer = new JsonSerializerStream();

        public Dictionary<string, string> MapItemToText;

        public NotificationData()
        {
            MapItemToText = new Dictionary<string, string>();
        }

        public NotificationData(Stream source)
        {
            Deserialize(source);
        }

        public Stream Serialize()
        {
            MemoryStream resultStream = new MemoryStream();

            serializer.Serialize(resultStream, MapItemToText);

            return resultStream;
        }

        public void Deserialize(Stream source)
        {
            MemoryStream resultStream = new MemoryStream();

            this.MapItemToText = serializer.Deserialize<Dictionary<string, string>>(source);
        }
    }
}
