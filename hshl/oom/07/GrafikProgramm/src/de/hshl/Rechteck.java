package de.hshl;

public class Rechteck extends GrafischesElement {

	public Rechteck(int x, int y) {
		super(x, y);
	}

	@Override
	public void zeichne() {
		System.out.println("Zeichne Rechteck.");
	}
}
