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

    public static void LoadFromFile(DataRepository repository, string filename)
    {
        if (!File.Exists(filename))
            return;

        string json = File.ReadAllText(filename);
        DataRepository result = JsonConvert.DeserializeObject<DataRepository>(json,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

        repository.Clear();
        foreach (var obj in result.Persons.Elements)
            repository.Persons.Save(obj);

        foreach (var obj in result.Seminars.Elements)
            repository.Seminars.Save(obj);
    }
}
