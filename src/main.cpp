#include <cstdlib>
#include <iostream>
#include <fstream>
#include "String.h"
#include "ArrayList.h"
#include "Map.h"
#include "GraphAL.h"
#include "GraphAM.h"
#include "BFS.h"
#include <unordered_map>

using namespace std;

int main() {
    GraphAM g(8);
    g.addEdge(1, 2);
    g.addEdge(1, 3);
    g.addEdge(2, 4);
    g.addEdge(2, 5);
    g.addEdge(3, 6);
    g.addEdge(3, 7);
    g.addEdge(5, 8);
    
    BFS bfs(&g);
    bfs.search(1);
    cout << bfs.getColor(8) << endl;
}