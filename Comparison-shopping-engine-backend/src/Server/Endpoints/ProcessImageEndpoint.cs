using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class encapsulates the expected behaviour of the endpoint, the responsibility of which is
    /// to parse photographed receipt into a Receipt object, and return it to the end user
    /// </summary>
    class ProcessImageEndpoint : IEndpoint
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Callback GetHandler()
        {
            return ProcessImage;
        }

        public string GetURI()
        {
            return "ProcessImage";
        }
    
        private string ProcessImage(Stream input)
        {
            Bitmap image = new Bitmap(input);

            Receipt result = Controller.ProcessImage(image);

            return serializer.Serialize(result);
        }
    }
}
