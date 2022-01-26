package de.hshl;

public class Person {
	protected String vorname;
	protected String Nachname;
	protected Adresse wohnadresse;

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
	
	public Adresse getWohnadresse() {
		return wohnadresse;
	}

	public void setWohnadresse(Adresse wohnadresse) {
		this.wohnadresse = wohnadresse;
	}
	
	public Person(String vorname, String nachname) {
		setVorname(vorname);
		setNachname(nachname);
	}
	
	@Override
	public String toString() {
		return getVorname() + " " + getNachname();
	}
}