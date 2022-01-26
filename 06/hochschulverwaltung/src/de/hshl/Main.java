package de.hshl;

public class Main {

	public static void main(String[] args) {
		Student s = new Student("Eva", "Ridlicka", 12345);
		System.out.println("Ein Student: " + s);

		Dozent d = new Dozent("Isabell", "Öztürk", "Mathemtik");
		System.out.println("Ein Dozent: " + d);
	
		Person p = s;
		System.out.println("Eine Person: " + p);
	}
}