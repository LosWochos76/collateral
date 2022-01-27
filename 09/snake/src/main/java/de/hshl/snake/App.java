package de.hshl.snake;

import javafx.application.Application;
import javafx.application.Platform;
import javafx.scene.Scene;
import javafx.scene.canvas.Canvas;
import javafx.scene.control.Alert;
import javafx.scene.control.Alert.AlertType;
import javafx.scene.layout.Pane;
import javafx.scene.paint.Color;
import javafx.stage.Stage;
import javafx.animation.*;

public class App extends Application {
	private Spielfeld feld;
	
    @Override
    public void start(Stage stage) {
        var root = new Pane();
        var canvas = new Canvas(640, 480);
        var gc = canvas.getGraphicsContext2D();
        
        root.getChildren().add(canvas);
        var scene = new Scene(root, 640, 480, Color.WHITESMOKE);
        
        scene.setOnKeyPressed(e -> {
        	feld.keyPressed(e.getCode());
        });

        stage.setTitle("Lines");
        stage.setScene(scene);
        stage.setResizable(false);
        stage.show();
        
        feld = new Spielfeld(gc);
        
        AnimationTimer timer = new AnimationTimer() {
            @Override
            public void handle(long now) {
            	if (!feld.isGameOver()) {
            		feld.tick(now);
            	} else {
            		var alert = new Alert(AlertType.ERROR);
            		alert.setContentText("Game over!");
            		alert.setOnHidden(evt -> Platform.exit());
            		alert.show();
            		stop();
            	}
            }
        };
        timer.start();
    }

    public static void main(String[] args) {
        launch();
    }
}