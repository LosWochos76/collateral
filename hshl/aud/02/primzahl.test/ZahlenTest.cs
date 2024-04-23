using System;
namespace Primzahl.Test;

public class ZahlenTest
{
    [Fact]
    public void istPrimzahl_0_ist_keine_Primzahl()
    {
        Assert.False(Zahlen.istPrimzahl(0));
    }

    [Fact]
    public void istPrimzahl_2_ist_Primzahl()
    {
        Assert.True(Zahlen.istPrimzahl(2));
    }

    [Fact]
    public void istPrimzahl_4_ist_keine_Primzahl()
    {
        Assert.False(Zahlen.istPrimzahl(4));
    }

    [Fact]
    public void istPrimzahl_23_ist_Primzahl()
    {
        Assert.True(Zahlen.istPrimzahl(23));
    }
}