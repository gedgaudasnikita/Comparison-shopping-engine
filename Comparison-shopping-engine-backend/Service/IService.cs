using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Comparison_shopping_engine_backend
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Receipt ProcessImage(Bitmap source);

        [OperationContract]
        List<Item> ProcessReceipt(Receipt source);
    }
}
