#ifndef BFS_H
#define	BFS_H

#include "Graph.h"
#include <unordered_map>

class BFS {
public:
    BFS(Graph* g);
    void explore(int start_node);
    int getDistance(int node);
    int getColor(int node);
    int getParent(int node);
    bool hasPath(int dest);
    std::list<int> getPath(int dest);

private:
    Graph* g;
    int start_node;
    std::unordered_map<int, int> color;
    std::unordered_map<int, int> distance;
    std::unordered_map<int, int> parent;
    void initialize();
};

#endif