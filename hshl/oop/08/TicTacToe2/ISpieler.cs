interface ISpieler
{
    char Spielstein { get; set; }
    void Ziehe(Spielfeld feld);
}