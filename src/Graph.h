#ifndef GRAPH_H
#define	GRAPH_H

#include <list>

class Graph {
public:
    virtual int nodeCount() = 0;
    virtual void addEdge(int u, int v) = 0;
    virtual void deleteEdge(int u, int v) = 0;
    virtual bool hasEdge(int u, int v) = 0;
    virtual void print() = 0;
    virtual std::list<int> nodesFrom(int node) = 0;
    std::string toDot();
    void toDotFile(const char* filename);
    void randomEdges(int edge_count);
};

#endif