package de.hshl;

public class Main {
	public static void tueEtwas(Person p) {
		System.out.println("Person");
	}
	
	public static void tueEtwas(Student p) {
		System.out.println("Person");
	}
	
	public static void main(String[] args) {
		Student s = new Student("Eva", "Ridlicka", 12345);
		System.out.println("Ein Student: " + s);

		Dozent d = new Dozent("Isabell", "Öztürk", "Mathemtik");
		System.out.println("Ein Dozent: " + d);
	
		// UpCasting:
		Person p = s;
		
		// Dynamisches Binden:
		System.out.println("Eine Person: " + p);
		
		// Das Double-Dispatch-Problem:
		tueEtwas(s);
		tueEtwas(p);
	}
}