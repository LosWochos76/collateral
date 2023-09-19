using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RandomDog;

public class DogService
{
    private HttpClient client;

    public DogService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ImageSource> LoadDog()
    {
        var dog = await client.GetFromJsonAsync<DogMessage>("https://dog.ceo/api/breeds/image/random");
        return dog.IsSuccess ? new BitmapImage(new Uri(dog.Message)) : null;
    }
}
