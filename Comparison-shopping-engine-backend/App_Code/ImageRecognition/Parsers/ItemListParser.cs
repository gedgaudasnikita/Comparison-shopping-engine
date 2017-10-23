using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// This class describes the behaviour of parsing an itemList from plain text.
/// Intended for usage from <see cref="Receipt"> class
/// </summary>
public class ItemListParser: IParser<List<Item>>
{
    //Lines ending with three numbers and tax groups A or M1 are what we consider items
    private string itemRegex = @"(.+)(\d.*\d.*\d.*)(?:A|M1)";

    /// <summary>
    /// Parses the given <see langword="string"/> value and returns the itemList
    /// </summary>
    /// <param name="source">The <see langword="string"/> to be parsed.</param>
    /// <returns>
    /// Returns a <see langword="List<>"/> of <see cref="Item"/>, with the information, 
    /// parsed from <paramref name="source"/>.
    /// </returns>
    public List<Item> Parse(string source)
    {
        List<Item> parsedItems = new List<Item>();

        var itemMatch = Regex.Match(source, itemRegex);

        while (itemMatch.Success)
        {
            parsedItems.Add(ParseItem(itemMatch));
            itemMatch = itemMatch.NextMatch();
        }

        return parsedItems;
    }

    /// <summary>
    /// A private helper method to parse one separate item. Used from <see cref="Parse"/>
    /// to create an Item for every discovered item entry.
    /// </summary>
    /// <param name="matchedItem">The <see langword="Regex.Match"/> for the current item entry.</param>
    /// <returns>
    /// Returns an <see cref="Item"/> using the information given in <paramref name="matchedItem"/>.
    /// </returns>
    private Item ParseItem(Match matchedItem)
    {
        //According to the specified regex, there are two matching groups (the last one is not matching)
        //Match.Group[0] is a full match
        //Match.Group[1] is the first matching group
        string itemName = matchedItem.Groups[1].Value.Trim();

        //Match.Group[2] is the second matching group
        int itemPrice;
        string itemPriceClean = matchedItem.Groups[2].Value.RemoveNonDigits();
        if (!int.TryParse(itemPriceClean, out itemPrice))
        {
            itemPrice = 0;
        }

        Item parsedItem = new Item(itemName, itemPrice);
        return parsedItem;
    }
}