package de.hshl;

public class Dreieck extends GrafischesElement {

	public Dreieck(int x, int y) {
		super(x, y);
	}

	@Override
	public void zeichne() {
		System.out.println("Zeichne Dreieck.");	
	}
}
