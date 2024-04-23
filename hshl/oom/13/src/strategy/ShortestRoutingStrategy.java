package strategy;

public class ShortestRoutingStrategy implements RoutingStrategy {

	@Override
	public Route navigate(String start, String dest) {
		return new Route(316, 214);
	}
}