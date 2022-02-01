package strategy;

public class Route {
	private double lenght;
	private double duration;
	
	public double getLenght() {
		return lenght;
	}
	
	public void setLenght(double lenght) {
		this.lenght = lenght;
	}
	
	public double getDuration() {
		return duration;
	}
	
	public void setDuration(double duration) {
		this.duration = duration;
	}

	public Route(double length, double duration) {
		setLenght(lenght);
		setDuration(duration);
	}
}
