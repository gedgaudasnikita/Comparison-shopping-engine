using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

[DataContract]
/// <summary>
/// A class representing the Receipt entity. Mainly used for encapsualting the results of image recognition.
/// </summary>
public class Receipt
{
    //Static fields
    //Mainly parsers, enabling Dependency Injection

    public static IParser<string> StoreParser { private get; set; }
    public static IParser<List<Item>> ItemListParser { private get; set; }
    public static IParser<DateTime> DateParser { private get; set; }

    //Instance fields
    //User provided data, encapsulated in one entity\

    [DataMember]
    public string Store { get; set; }
    [DataMember]
    public List<Item> Items { get; set; }
    [DataMember]
    public DateTime Date { get; set; }

    /// <summary>
    /// Converts a given <see langword="Bitmap"/> to a Receipt entity with the parsed information
    /// </summary>
    /// <param name="source">A <see langword="Bitmap"/> to convert from</param>
    /// <returns>A converted <see cref="Receipt"/></returns>
    public static Receipt Convert(Bitmap source)
    {
        var text = OCRWrapper.ConvertToText(source);
        return Parse(text);
    }

    /// <summary>
    /// Creates a new <see cref="Receipt"/> instance and fills it with information, parsed from
    /// <paramref name="source"/>, using the supplied parsers, specific to each relative field of
    /// <see cref="Receipt"/>
    /// </summary>
    /// <param name="source">A <see langword="string"/> to parse</param>
    /// <returns>A newly created <see cref="Receipt"/></returns>
    public static Receipt Parse(string source)
    {
        Receipt result = new Receipt();

        // In perspective, null cases should throw a custom exception - this should not be a normal flow
        if (StoreParser != null)
        {
            result.Store = StoreParser.Parse(source);
        }

        if (ItemListParser != null)
        {
            result.Items = ItemListParser.Parse(source);
        }

        if (DateParser != null)
        {
            result.Date = DateParser.Parse(source);
        }

        //Make sure the dates and stores are all the same
        foreach (Item item in result.Items)
        {
            item.Date = result.Date;
            item.Store = result.Store;
        }
          
        return result;
    }

    public Receipt()
    {
        Store = "";
        Date = new DateTime().Date;
        Items = new List<Item>();
    }
}
