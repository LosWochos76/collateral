using System.Text.Json.Serialization;

namespace RandomDog;

public class DogMessage
{
    public string Message { get; set; }
    public string Status { get; set; }

    [JsonIgnore]
    public bool IsSuccess { get { return Status.Equals("success"); } }
}
