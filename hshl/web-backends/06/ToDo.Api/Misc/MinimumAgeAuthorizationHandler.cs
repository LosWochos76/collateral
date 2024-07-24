using Microsoft.AspNetCore.Authorization;

namespace ToDoManager.Api.Misc;

public class MimimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        var ageClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Age");
        if (ageClaim is null)
        {            
            context.Fail();
            return Task.CompletedTask;
        }
        
        var age = Convert.ToInt32(ageClaim.Value);
        if (requirement.MinimumAge > age)
            context.Fail();
        
        return Task.CompletedTask;
    }
}