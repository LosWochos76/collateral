package de.hshl;

public class Student extends Person {
	protected int matrikelnummer;

	public int getMatrikelnummer() {
		return matrikelnummer;
	}

	public void setMatrikelnummer(int matrikelnummer) {
		this.matrikelnummer = matrikelnummer;
	}
	
	public Student(String vorname, String nachname, int matrikelnummer) {
		super(vorname, nachname);
		setMatrikelnummer(matrikelnummer);
	}
	
	public String toString() {
		return getVorname() + " " + getNachname() + " - " + getMatrikelnummer();
	}
}