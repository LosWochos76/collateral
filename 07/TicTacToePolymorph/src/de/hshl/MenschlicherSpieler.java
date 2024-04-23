package de.hshl;

import java.util.Scanner;

public class MenschlicherSpieler extends Spieler {
	private Scanner in = new Scanner(System.in);

	public MenschlicherSpieler(char spielstein) {
		super(spielstein);
	}

	@Override
	public void ziehe(Spielfeld feld) {
		int x, y;
		
		do {
			x = leseKoordinate("Bitte X-Koordinate eingeben (0-2): ");
			y = leseKoordinate("Bitte Y-Koordinate eingeben (0-2): ");
		} while (x < 0 || x > 2 || y < 0 || y > 2 || feld.istBelegt(x, y));
		
		feld.setzeSpielstein(x, y, spielstein);
	}
	
	private int leseKoordinate(String text) {
		System.out.println(text);
		return in.nextInt();
	}
}