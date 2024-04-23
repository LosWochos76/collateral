package de.hshl;

public abstract class GrafischesElement {
	protected int x;
	protected int y;

	public int getX() {
		return x;
	}

	public void setX(int x) {
		this.x = x;
	}

	public int getY() {
		return y;
	}

	public void setY(int y) {
		this.y = y;
	}
	
	public void bewege(int delta_x, int delta_y) {
		x += delta_x;
		y += delta_y;
	}
	
	public abstract void zeichne();
	
	public GrafischesElement(int x, int y) {
		setX(x);
		setY(y);
	}
}
