package de.hshl;

public class Main {
    public static void main(String[] args) throws Exception {
        TicTacToe ttt = new TicTacToe();
        ttt.setSpieler1(new ComputerSpieler('X'));
        ttt.setSpieler2(new MenschlicherSpieler('O'));
        ttt.starteSpiel();
    }
}