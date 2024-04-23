package de.hshl.singleton;

public class DatabaseConnection {
   private static DatabaseConnection instance = null;

   private DatabaseConnection() {
   }

   public static DatabaseConnection getInstance() {
      if (instance == null) {
         instance = new DatabaseConnection();
         // Hier können noch weitere Dinge getan werden, zB Verbindungsaufbau
      }

      return instance;
   }
}
