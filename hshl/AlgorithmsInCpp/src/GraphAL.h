#ifndef GRAPHAL_H
#define	GRAPHAL_H

#include "Graph.h"
#include <list>
#include <string>

// Graph using a adjacency-list to store the graph
class GraphAL : public Graph {
public:
    GraphAL(int node_count);
    int nodeCount();
    void addEdge(int u, int v);
    void deleteEdge(int u, int v);
    bool hasEdge(int u, int v);
    void print();
    std::list<int> nodesFrom(int node);
    
private:
    int node_count;
    std::list<int> *adjacency_list;
};

#endif