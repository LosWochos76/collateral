using Microsoft.AspNetCore.Authorization;

public class HurgaMinimumAgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; set; }

    public MinimumAgeRequirement(int minimumAge)
    {
        MinimumAge = minimumAge;
    }
}