#include <iostream>
#include "GraphAL.h"
#include "GraphAM.h"
#include "DFS.h"

using namespace std;

int main() {
    GraphAL g(8);
    g.addEdge(1, 2);
    g.addEdge(1, 3);
    g.addEdge(2, 4);
    g.addEdge(2, 5);
    g.addEdge(3, 6);
    g.addEdge(3, 7);
    g.addEdge(5, 8);
    
    DFS dfs(&g);
    dfs.explore();

    cout << "Number of components: " << dfs.getNoOfComponents() << endl;
    cout << "Graph has cycles: " << dfs.isCyclic() << endl;
    
    list<int> order = dfs.getTopologicOrder();
    while (!order.empty()) {
        cout << order.front() << " -> ";
        order.pop_front();
    }
}