package strategy;

public class FastestRoutingStrategy implements RoutingStrategy {

	@Override
	public Route navigate(String start, String dest) {
		return new Route(323, 202);
	}

}
