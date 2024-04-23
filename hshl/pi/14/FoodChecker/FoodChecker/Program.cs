using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckSpecificFood();
            Console.ReadLine();
        }

        public static async void CheckSpecificFood()
        {
            var list = new List<string>() { "Schnitzel", "Pommes", "Currywurst", "Burger", "Gyros" };
            bool result = await CheckFoodAsync(list);

            if (result)
                Console.WriteLine("Yeah!");
            else
                Console.WriteLine("Nohhh!");
        }

        public static async Task<bool> CheckFoodAsync(List<string> keywords)
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://www.studierendenwerk-pb.de/gastronomie/speiseplaene/mensa-basilica-hamm/");
            var content = await result.Content.ReadAsStringAsync();

            var text = content.ToUpper();
            foreach (var word in keywords)
                if (text.Contains(word.ToUpper()))
                    return true;

            return false;
        }
    }
}
