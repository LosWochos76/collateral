public class Routeplaner
{
    public IRouteStrategy Strategy { get; set; }

    public Routeplaner(IRouteStrategy strategy) 
    {
        Strategy = strategy;
    }

    public Route ExecuteStrategy()
    {
        return Strategy.GetRoute();
    }
}