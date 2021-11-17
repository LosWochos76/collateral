using System;

public class ShortestRoutingStrategy : IRouteStrategy
{
    public Route GetRoute()
    {
        return new Route
        {
            Path = "a1->c1->b3",
            Duration = DateTime.UtcNow.AddHours(1.5) - DateTime.UtcNow
        };
    }
}