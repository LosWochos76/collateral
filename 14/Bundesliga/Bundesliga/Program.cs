using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Bundesliga
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            PrintAverageHomeGoals();
            Console.ReadLine();
        }

        public static async void PrintAverageHomeGoals()
        {
            var matchList = await LoadMatches();
            Console.WriteLine("Durchschn. Heimtore={0:F2}", matchList.AverageHomeGoals);
        }

        public static async Task<MatchList> LoadMatches() 
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://www.openligadb.de/api/getmatchdata/bl1/2017");

            JArray array = JArray.Parse(result);
            var matches = array.ToObject<List<Match>>();

            return new MatchList(matches);
        }
    }
}