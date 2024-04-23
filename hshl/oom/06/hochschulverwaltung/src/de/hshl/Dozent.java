package de.hshl;

public class Dozent extends Person {
	protected String lehrgebiet;

	public String getLehrgebiet() {
		return lehrgebiet;
	}

	public void setLehrgebiet(String lehrgebiet) {
		this.lehrgebiet = lehrgebiet;
	}
	
	public Dozent(String vorname, String nachname, String lehrgebiet) {
		super(vorname, nachname);
		setLehrgebiet(lehrgebiet);
	}
	
	@Override
	public String toString() {
		return getVorname() + " " + getNachname() + " - " + getLehrgebiet();
	}
}
