using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

public class Service : IService
{
    public Receipt TestMethod()
    {
        return new Receipt();
    }
    
    public Receipt ProcessImage(Bitmap source)
    {
        Receipt receipt = Receipt.Convert(source);

        var normalizer = NormalizationEngine.GetInstance();
        receipt.Items.ForEach(item => item.Name = normalizer.GetClosest(item.Name));

        return receipt;
            
    }

    public List<Item> ProcessReceipt(Receipt source)
    {
        ItemManager manager = ItemManager.GetInstance();
        manager.Add(source.Items);

        manager.Persist();

        List<Item> cheapList = source.Items.Select(item => manager.FindCheapest(item)).ToList();

        return cheapList;
    }
}
