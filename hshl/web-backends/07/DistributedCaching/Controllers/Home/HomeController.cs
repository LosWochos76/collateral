using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

public class HomeController(IHttpClientFactory httpClientFactory, IDistributedCache cache) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> WithCaching()
    {
        var cached_sum = await cache.GetStringAsync("goal_count");
        if (cached_sum is not null)
            return View("Output", cached_sum);

        var client = httpClientFactory.CreateClient();
        var matches = await client.GetFromJsonAsync<IEnumerable<Match>>("https://api.openligadb.de/getmatchdata/bl1/2023/");

        long sum = 0;
        foreach (var match in matches)
            sum += match.MatchResults.Last().PointsTeam1 + match.MatchResults.Last().PointsTeam2;

        await cache.SetStringAsync("goal_count", sum.ToString(), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(10) });
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