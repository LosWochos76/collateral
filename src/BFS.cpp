#include "BFS.h"
#include <stack>

BFS::BFS(Graph* g) {
    this->g = g;
}

void BFS::initialize() {
    for (int i=1; i<=g->nodeCount(); i++) {
        color[i] = WHITE;
        distance[i] = -1;
        parent[i] = -1;
    }
}

void BFS::search(int node) {
    initialize();
    
    distance[node] = 0;
    std::stack<int> nodes;
    nodes.push(node);
    
    while (!nodes.empty()) {
        int u = nodes.top();
        nodes.pop();
        std::list<int> adj = g->nodesFrom(u);
        while (!adj.empty()) {
            int v = adj.front();
            adj.pop_front();
            if (color[v] == WHITE) {
                color[v] = GRAY;
                distance[v] = distance[u] + 1;
                parent[v] = u;
                nodes.push(v);
            }
        }
        
        color[u] = BLACK;
    }
}

int BFS::getColor(int node) {
    return color[node];
}

int BFS::getDistance(int node) {
    return distance[node];
}

int BFS::getParent(int node) {
    return parent[node];
}