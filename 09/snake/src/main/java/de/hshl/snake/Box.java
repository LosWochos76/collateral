package de.hshl.snake;

import java.util.Random;

import javafx.scene.canvas.GraphicsContext;
import javafx.scene.paint.*;

public class Box {
	public static final int WIDTH = 15;
	private int x;
	private int y;
	private static Random random = new Random();
	
	public Box(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public void paint(GraphicsContext gc) {
    	gc.setFill(Color.GREEN);
        gc.fillRect(x, y, WIDTH, WIDTH);
    	gc.setStroke(Color.RED);
    	gc.strokeRect(x, y, WIDTH, WIDTH);
	}
	
	public static Box newBox(Box old_box, Direction d) {
		if (d == Direction.up)
			return new Box(old_box.x, old_box.y-WIDTH);
		
		if (d == Direction.down)
			return new Box(old_box.x, old_box.y+WIDTH);
		
		if (d == Direction.left)
			return new Box(old_box.x-WIDTH, old_box.y);
		
		if (d == Direction.right)
			return new Box(old_box.x+WIDTH, old_box.y);
		
		return null;
	}
	
	public boolean hasCollidedWithWalls() {
		return (x < 0 || x+WIDTH > 640 || y < 0 || y+WIDTH > 480);
	}
	
	public static Box randomBox() {
		return new Box(random.nextInt(41) * WIDTH, random.nextInt(31) * WIDTH);
	}
	
	@Override
	public boolean equals(Object o) {
	    if (this == o) return true;
	    if (o instanceof Box) {
	    	var b = (Box)o;
	        return b.x == x && b.y == y;
	    } else {
	        return false;
	    }
	}
}
