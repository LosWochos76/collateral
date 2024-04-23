package de.hshl;

import java.util.List;

public class CsvParser {
	public static SalesPositionList parse(List<String> lines) {
		var result = new SalesPositionList();
		
		for (int i=1; i<lines.size(); i++) {
			var pos = SalesPosition.fromLine(lines.get(i));
			result.add(pos);
		}
		
		return result;
	}
}
