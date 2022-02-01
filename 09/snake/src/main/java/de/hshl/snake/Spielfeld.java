package de.hshl.snake;

import javafx.application.Platform;
import javafx.scene.canvas.GraphicsContext;
import javafx.scene.input.KeyCode;

public class Spielfeld {
	private GraphicsContext gc;
	private Schlange schlange;
	private int points = 0;
	private long last_tick = 0;
	private Box apple;
	
	public Spielfeld(GraphicsContext gc) {
		this.gc = gc;
		this.apple = Box.randomBox();
		this.schlange = new Schlange();
	}
	
	public void tick(long now) {
		if (now - last_tick < 100_000_000)
			return;
		
		gc.clearRect(0, 0, 640, 480);
		schlange.move();
		
		if (schlange.eats(apple)) {
			schlange.increaseLenght();
			apple = Box.randomBox();
			points++;
		}
		
		schlange.paint(gc);
		apple.paint(gc);
		
		last_tick = now;
	}
	
	public boolean isGameOver() {
		return schlange.isGameOver();
	}
	
	public void keyPressed(KeyCode code) {
		if (code == KeyCode.UP)
			schlange.setDirection(Direction.up);
		
		if (code == KeyCode.DOWN)
			schlange.setDirection(Direction.down);
		
		if (code == KeyCode.LEFT)
			schlange.setDirection(Direction.left);
		
		if (code == KeyCode.RIGHT)
			schlange.setDirection(Direction.right);
		
		if (code == KeyCode.ESCAPE)
			Platform.exit();
	}
}
