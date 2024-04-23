package de.hshl;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;

public class Main {
	public static void main(String[] args) throws IOException {
		var lines = Files.readAllLines(Paths.get("Daten.csv"));
		var positions = CsvParser.parse(lines);
		System.out.println(positions.getTurnover());
	}
}
