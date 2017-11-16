using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comparison_shopping_engine_web_protocol
{
    /// <summary>
    /// The class, containing all the custom extension methods for the <see cref="JsonSerializer"/> type
    /// </summary>
    public class JsonSerializerStream: JsonSerializer
    {
        /// <summary>
        /// An overload, that takes <see cref="Stream"/> as the first argument instead of <see cref="JsonWriter"/>
        /// Avoids DRY. 
        /// </summary>
        /// <param name="output">An output <see cref="Stream"/></param>
        /// <param name="value">An object to be serialized</param>
        public void Serialize(Stream output, object value)
        {
            JsonWriter writer = new JsonTextWriter(new StreamWriter(output));

            Serialize(writer, value);

            writer.Flush();
            output.Position = 0;
        }

        /// <summary>
        /// An overload, that takes <see cref="Stream"/> as the argument instead of <see cref="JsonReader"/>
        /// Avoids DRY. 
        /// </summary>
        /// <param name="input">An input <see cref="Stream"/></param>
        public T Deserialize<T>(Stream input)
        {
            JsonReader reader = new JsonTextReader(new StreamReader(input));
            return Deserialize<T>(reader);
        }
    }
}
