var hvn = HausVomNikolaus.Erzeuge();
var dfs = new DepthFirstSearch(hvn);
dfs.Explore();
Console.WriteLine("HasCycle={0}, Components={1}", dfs.HasCycle, dfs.Components);