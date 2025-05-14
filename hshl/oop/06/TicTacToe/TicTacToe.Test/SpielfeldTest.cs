namespace TicTacToe.Test;

public class SpielfeldTest
{
    [Fact]
    public void HatGewonnen_Diagonal_von_links_oben_nach_rechts_unten()
    {
        var brett = new Spielfeld();
        Assert.False(brett.HatGewonnen('X'));
        Assert.False(brett.HatGewonnen('O'));
        
        brett.Setzen(0,0,'X');
        brett.Setzen(1,1,'X');
        brett.Setzen(2,2,'X');

        Assert.True(brett.HatGewonnen('X'));
        Assert.False(brett.HatGewonnen('O'));
    }
}