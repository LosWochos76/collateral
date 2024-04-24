#ifndef GRAPHAM_H
#define	GRAPHAM_H

#include "Graph.h"
#include <list>

// Graph using a adjacency-matrix to store the graph
class GraphAM : public Graph {
public:
    GraphAM(int count);
    int nodeCount();
    void addEdge(int u, int v);
    void deleteEdge(int u, int v);
    bool hasEdge(int u, int v);
    void print();
    std::list<int> nodesFrom(int node);

private:
    int node_count; 
    int *adjacency_matrix;
};

#endif