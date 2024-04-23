using System;

public class FastestRoutingStrategy : IRouteStrategy
{
    public Route GetRoute()
    {
        return new Route
        {
            Path = "a1->a2->b3",
            Duration = DateTime.UtcNow.AddHours(1) - DateTime.UtcNow
        };
    }
}