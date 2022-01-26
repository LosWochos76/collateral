package de.hshl;

public class Person {
	protected String vorname;
	protected String Nachname;
	
	public String getVorname() {
		return vorname;
	}
	
	public void setVorname(String vorname) {
		this.vorname = vorname;
	}
	
	public String getNachname() {
		return Nachname;
	}
	
	public void setNachname(String nachname) {
		Nachname = nachname;
	}
	
	public Person(String vorname, String nachname) {
		setVorname(vorname);
		setNachname(nachname);
	}
	
	public String toString() {
		return getVorname() + " " + getNachname();
	}
}