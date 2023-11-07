/*var heap = new MaxHeap<int>(new int[]{22,3,1,9,8,0,13});
while (heap.HasElements)
    Console.WriteLine(heap.ExtractMax());
*/

var content = FileHelper.ReadAsChars("alice-in-wonderland.txt");
var freq = FileHelper.GetFrequencies(content);

var hm = new Hufman(freq);
var binary = hm.Compress(content);
Console.WriteLine("Ersparnis={0:p}", 1 - (binary.Length / 8) / (double)content.Length);