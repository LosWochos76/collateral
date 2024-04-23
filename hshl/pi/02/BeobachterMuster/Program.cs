var b = new KontoBeobachter();

var k1 = new Bankkonto();
k1.BeobachterAnmelden(b);
var k2 = new Bankkonto();
k2.BeobachterAnmelden(b);

k1.Einzahlen(200);
k2.Einzahlen(500);
k1.Auszahlen(50);
k2.Auszahlen(75);

