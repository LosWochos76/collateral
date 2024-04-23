using System.Net.Http.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("https://api.openligadb.de");

var matches = await client.GetFromJsonAsync<IEnumerable<Match>>("/getmatchdata/bl1/2023/1");
foreach (var m in matches)
    Console.WriteLine(m.Team1.shortName + " -- " + m.Team2.shortName);