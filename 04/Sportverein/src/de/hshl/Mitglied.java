package de.hshl;

import java.time.LocalDate;

public class Mitglied {
    private String vorname;
    private String nachname;
    private LocalDate geburtstag;
  
    public String getVorname() { return vorname; }
    public void setVorname(String vorname) { this.vorname = vorname; }
    public String getNachname() { return nachname; }
    public void setNachname(String nachname) { this.nachname = nachname; }
    public LocalDate getGeburtstag() { return geburtstag; }
    public void setGeburtstag(LocalDate geburtstag) { this.geburtstag = geburtstag; }
    public void setGeburtstag(String geburtstag) {
      setGeburtstag(LocalDate.parse(geburtstag));
    }
  
    public Mitglied(String v, String n, String g) {
      setVorname(v);
      setNachname(n);
      setGeburtstag(g);
    }
}