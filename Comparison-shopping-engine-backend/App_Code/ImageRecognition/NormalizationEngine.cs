using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

/// <summary>
/// This class is used for normalisation of <see cref="string"/> names and storing the fixed names.
/// Intended for OCR result correction and autocomplete suggestion mechanisms.
/// </summary>
public class NormalizationEngine
{
    private static NormalizationEngine instance = null;
    private List<string> names;
    private string normalizationDir;

    private NormalizationEngine()
    {
        names = new List<string>();
        normalizationDir = ConfigurationManager.AppSettings["normalizationDir"];
    }

    public static NormalizationEngine GetInstance()
    {
        if (instance == null)
        {
            instance = new NormalizationEngine();
        }
        return instance;
    }

    /// <summary>
    /// Add a new fixed name into the name storage. Should only be used with confirmed names,
    /// for example, the ones manually entered by User. 
    /// </summary>
    /// <param name="name">The <see cref="string"/> to be added</param>
    public void AddName(string name)
    {
        names.Add(name);
    }

    /// <summary>
    /// Check if a given name is stored
    /// </summary>
    /// <param name="name">The <see cref="string"/> to be checked</param>
    /// <returns>A <see cref="bool"/> indicating whether the name is stored or not</returns>
    public bool Exists(string name)
    {
        return names.Contains(name);
    }

    /// <summary>
    /// Clear the internal list
    /// </summary>
    public void ClearList()
    {
        names.Clear();
    }

    /// <summary>
    /// Get a closest string match from the name storage. Intended to use for initial data normalisation.
    /// </summary>
    /// <param name="name">A <see cref="string"/> to be matched</param>
    /// <returns>A <see cref="string"/> with the closest match</returns>
    public string GetClosest(string name)
    {
        if (names.Count == 0)
        {
            return "";
        } else
        {
            //Ensures we only iterate through the collection once.
            return names.Aggregate((name1, name2) => name1.GetDistance(name) > name2.GetDistance(name) ? name2 : name1);
        }
    }

    /// <summary>
    /// Get a list of the closest matches for a particular name. Intended to use for generating suggestions.
    /// In case where <paramref name="limit"/> is 1, it is recommended to use <see cref="getClosest(string)"/>
    /// for more efficient implementation.
    /// </summary>
    /// <param name="name">A <see cref="string"/> to be matched</param>
    /// <param name="limit">An <see cref="int"/>, indicating the preferred amount of matches to return</param>
    /// <returns>A <see cref="List{string}"/> of the closest matches</returns>
    public IEnumerable<string> GetClosestList(string name, int limit)
    {
        return names.OrderBy(savedName => savedName.GetDistance(name)).Take(limit);
    }

    /// <summary>
    /// A method serializing all of the names currently stored
    /// to a User configured location.
    /// Deletes the old data stored before writing.
    /// </summary>
    public void Persist()
    {
        DirectoryInfo normalizationDirInfo = new DirectoryInfo(normalizationDir);

        if (normalizationDirInfo.Exists)
        {
            foreach (FileInfo file in normalizationDirInfo.GetFiles())
            {
                file.Delete();
            }
        }
        else
        {
            System.IO.Directory.CreateDirectory(normalizationDir);
        }

        JavaScriptSerializer serializer = new JavaScriptSerializer();
            
        string filename = normalizationDir + "/names.list";
        File.WriteAllText(filename, serializer.Serialize(names));
    }

    /// <summary>
    /// A method deserializing all of the names currently saved
    /// from a User configured location.
    /// Deletes all the old names before reading.
    /// </summary>
    public void LoadAll()
    {
        ClearList();

        DirectoryInfo dirInfo = new DirectoryInfo(normalizationDir);

        if (!dirInfo.Exists)
        {
            Directory.CreateDirectory(normalizationDir);
        }

        FileInfo fileInfo = new FileInfo(normalizationDir + "/names.list");

        JavaScriptSerializer serializer = new JavaScriptSerializer();
        if (fileInfo.Exists)
        {
            string serialized = File.ReadAllText(normalizationDir + "/names.list");
            names = serializer.Deserialize<List<string>>(serialized) ?? names;
        }
    }
}
