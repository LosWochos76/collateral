package de.hshl.observer;

import java.beans.PropertyChangeListener;
import java.beans.PropertyChangeSupport;

public class Bankkonto {
   private double kontostand = 0;
   private PropertyChangeSupport changes = new PropertyChangeSupport(this);
   
   public void einzahlen(double betrag) {
      var alter_kontostand = kontostand;
      kontostand += betrag;
      changes.firePropertyChange("kontostand", alter_kontostand, kontostand);
   }
   
   public void auszahlen(double betrag) {
      var alter_kontostand = kontostand;
      kontostand -= betrag;
      changes.firePropertyChange("kontostand", alter_kontostand, kontostand);
   }
   
   public double getKontostand() {
      return kontostand;
   }
   
   public void addPropertyChangeListener(PropertyChangeListener l) {
      changes.addPropertyChangeListener(l);
   }
   
   public void removePropertyChangeListener(PropertyChangeListener l) {
      changes.removePropertyChangeListener(l);
   }
}