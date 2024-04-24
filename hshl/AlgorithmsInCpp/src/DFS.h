#ifndef DFS_H
#define	DFS_H

#include "Graph.h"
#include <unordered_map>

class DFS {
public:
    DFS(Graph *g);
    void explore();
    void visit(int node);
    int getColor(int node);
    int getParent(int node);
    bool isCyclic();
    int getNoOfComponents();
    std::list<int> getTopologicOrder();

private:
    void initialize();
    Graph* g;
    std::unordered_map<int, int> color;
    std::unordered_map<int, int> parent;
    std::unordered_map<int, int> component;
    std::list<int> topolic;
    bool has_cycle;
    int components;
};

#endif