using Newtonsoft.Json;
using System.IO;

namespace SeminarManager;

public class JsonSerializer
{
    public static void SaveToFile(DataRepository repository, string filename)
    {
        string json = JsonConvert.SerializeObject(repository,
        new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        });

        File.WriteAllText(filename, json);
    }

    public static DataRepository LoadFromFile(string filename)
    {
        if (!File.Exists(filename))
            return null;

        string json = File.ReadAllText(filename);
        DataRepository result = JsonConvert.DeserializeObject<DataRepository>(json,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

        return result;
    }
}
