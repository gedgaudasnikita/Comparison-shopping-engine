using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

[ServiceContract]
public interface IService
{
    [OperationContract]
    Receipt TestMethod();

    [OperationContract]
    Receipt ProcessImage(Bitmap source);

    [OperationContract]
    List<Item> ProcessReceipt(Receipt source);
}
