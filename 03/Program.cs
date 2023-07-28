var k = new Bankkonto();
k.Einzahlen(100);
k.Auszahlen(50);
Console.WriteLine("Der aktguelle Kontostand ist {0:C}.", k.Kontostand);