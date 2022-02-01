package de.hshl.snake;

import java.util.ArrayList;

import javafx.scene.canvas.GraphicsContext;

public class Schlange {
	private ArrayList<Box> body = new ArrayList<Box>();
	private Direction direction = Direction.up;
	private boolean game_over = false;
	private boolean increase_length = false;
	
	public Schlange() {
		body.add(new Box(13 * Box.WIDTH, 13 * Box.WIDTH));
		body.add(new Box(13 * Box.WIDTH, 14 * Box.WIDTH));
		body.add(new Box(13 * Box.WIDTH, 15 * Box.WIDTH));
	}
	
	private Box getHead() {
		return body.get(0);
	}
	
	public boolean isGameOver() {
		return game_over;
	}
	
	public void setDirection(Direction direction) {
		this.direction = direction;
	}
	
	public void move() {
		if (game_over)
			return;

		var new_head = Box.newBox(getHead(), direction);
		body.add(0, new_head);
		
		if (!increase_length)
			body.remove(body.size()-1);
			
		increase_length = false;
		
		if (hasCollidedWithWalls() || hasCollidedWithItself())
			game_over = true;
	}

	public void paint(GraphicsContext gc) {
		for (var box : body) {
			box.paint(gc);
		}
	}
	
	public boolean eats(Box apple) {
		return getHead().equals(apple);
	}
	
	public void increaseLenght() {
		increase_length = true;
	}
	
	private boolean hasCollidedWithWalls() {
		return getHead().hasCollidedWithWalls();
	}
	
	private boolean hasCollidedWithItself() {
		for (int i=1; i<body.size(); i++)
			if (getHead().equals(body.get(i)))
				return true;
		
		return false;
	}
}