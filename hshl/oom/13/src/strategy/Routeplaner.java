package strategy;

public class Routeplaner {
	private RoutingStrategy strategy;
	
	public void setStrategy(RoutingStrategy strategy) {
		this.strategy = strategy;
	}
	
	public Route navigate(String start, String dest) {
		return strategy.navigate(start, dest);
	}
}
