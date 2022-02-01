package de.hshl.observer;

import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;

public class BankkontoBeobachter implements PropertyChangeListener {

   @Override
   public void propertyChange(PropertyChangeEvent evt) {
      if (evt.getPropertyName().equals("kontostand")) {
         System.out.println("Der Kontostand hat sich geändert! Alt: " + evt.getOldValue() + ", Neu: " + evt.getNewValue());
      }
   }
}
