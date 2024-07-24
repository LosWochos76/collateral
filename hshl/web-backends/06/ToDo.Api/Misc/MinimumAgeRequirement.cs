using Microsoft.AspNetCore.Authorization;

namespace ToDoManager.Api.Misc;

public class MinimumAgeRequirement : IAuthorizationRequirement
{
    public int MinimumAge { get; set; }

    public MinimumAgeRequirement(int minimumAge)
    {
        MinimumAge = minimumAge;
    }
}