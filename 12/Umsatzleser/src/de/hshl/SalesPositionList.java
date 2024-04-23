package de.hshl;

import java.util.ArrayList;

public class SalesPositionList extends ArrayList<SalesPosition> {
	
	public double getTurnover() {
		double sum = 0;
		for (var obj : this) {
			sum += obj.getTurnover();
		}
		
		return sum;
	}
}
