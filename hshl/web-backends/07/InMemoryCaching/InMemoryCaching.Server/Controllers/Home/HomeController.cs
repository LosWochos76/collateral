using Bogus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Server;

public class HomeController(IMemoryCache cache, IHttpClientFactory httpClientFactory) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> WithCaching()
    {
        if (cache.TryGetValue("count", out long cached_sum))
            return View("Output", cached_sum);

        var client = httpClientFactory.CreateClient();
        var matches = await client.GetFromJsonAsync<IEnumerable<Match>>("https://api.openligadb.de/getmatchdata/bl1/2023/");

        long sum = 0;
        foreach (var match in matches)
            sum += match.MatchResults.Last().PointsTeam1 + match.MatchResults.Last().PointsTeam2;

        cache.Set("count", sum, new MemoryCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(10) });
        return View("Output", sum);
    }

    public async Task<IActionResult> WithoutCaching()
    {
        var client = httpClientFactory.CreateClient();
        var matches = await client.GetFromJsonAsync<IEnumerable<Match>>("https://api.openligadb.de/getmatchdata/bl1/2023/");

        long sum = 0;
        foreach (var match in matches)
            sum += match.MatchResults.Last().PointsTeam1 + match.MatchResults.Last().PointsTeam2;

        return View("Output", sum);
    }
}
