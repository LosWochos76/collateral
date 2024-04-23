package de.hshl.factory;

public class PersonFactory {
	public static Person erzeugePersonAusDaten(String line) {
		var parts = line.split(";");
		if (parts.length < 2)
			return null;
		
		if (parts.length == 3)
		{
			try {
				return new Student(parts[0], parts[1], Integer.parseInt(parts[2]));
			} 
			catch (Exception e)
			{
			}
			
			return new Dozent(parts[0], parts[1], parts[2]);
		}
		
		return new Person(parts[0], parts[1]);
	}
}