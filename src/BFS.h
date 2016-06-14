#ifndef BFS_H
#define	BFS_H

#include "Graph.h"
#include <unordered_map>

const int WHITE = 0;
const int GRAY = 1;
const int BLACK= 2;

class BFS {
public:
    BFS(Graph* g);
    void search(int node);
    int getDistance(int node);
    int getColor(int node);
    int getParent(int node);

private:
    Graph* g;
    std::unordered_map<int, int> color;
    std::unordered_map<int, int> distance;
    std::unordered_map<int, int> parent;
    void initialize();
};

#endif