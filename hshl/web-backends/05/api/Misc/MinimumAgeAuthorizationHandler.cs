using Microsoft.AspNetCore.Authorization;

public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        // our users do not have a birthdate-claim yet, so the policy only succeeds once such a claim is added
        var birthdateClaim = context.User.FindFirst("birthdate");
        if (birthdateClaim is null || !DateOnly.TryParse(birthdateClaim.Value, out var birthdate))
            return Task.CompletedTask;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - birthdate.Year;
        if (birthdate > today.AddYears(-age))
            age--;

        if (age >= requirement.MinimumAge)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
