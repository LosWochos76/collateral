using BundesligaDatenLader.Model;
using System.Text.Json;

namespace BundesligaDatenLader;

internal class OpenLigaClient
{
    public OpenLigaClient() { }

    public async Task<IEnumerable<Match>> GetMatches(string league, int season)
    {
        using (var client = new HttpClient())
        {
            var url = $"https://api.openligadb.de/getmatchdata/{league}/{season}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Match>>(content);
        }
    }

    public async Task<IEnumerable<Team>> GetTeams(string league, int season)
    {
        using (var client = new HttpClient())
        {
            var url = $"https://api.openligadb.de/getavailableteams/{league}/{season}";
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Team>>(content);
        }
    }
}
