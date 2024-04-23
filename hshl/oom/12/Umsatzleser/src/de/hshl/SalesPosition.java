package de.hshl;

import java.text.NumberFormat;
import java.util.Locale;

public class SalesPosition {
	private static NumberFormat format = NumberFormat.getInstance(Locale.GERMANY);
	private String name;
	private int count;
	private double price;
	
	public String getName() {
		return name;
	}
	
	public void setName(String name) {
		this.name = name;
	}
	
	public int getCount() {
		return count;
	}
	
	public void setCount(int count) {
		this.count = count;
	}
	
	public double getPrice() {
		return price;
	}
	
	public void setPrice(double price) {
		this.price = price;
	}
	
	public double getTurnover() {
		return price * count;
	}
	
	public static SalesPosition fromLine(String line) {
		var parts = line.split(";");
		SalesPosition result = null;
		
		try {
			result = new SalesPosition();
			result.setName(parts[0]);
			result.setPrice(format.parse(parts[1]).doubleValue());
			result.setCount(Integer.valueOf(parts[2]));
		} 
		catch (Exception e)
		{
		}
		
		return result;
	}
}