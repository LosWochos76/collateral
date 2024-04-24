#include "BFS.h"
#include <stack>
#include <iostream>

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

void BFS::explore(int start_node) {
    this->start_node = start_node;
    initialize();
    
    distance[start_node] = 0;
    std::stack<int> nodes;
    nodes.push(start_node);
    
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

bool BFS::hasPath(int node) {
    return node >= 1 && node <= g->nodeCount() && distance[node] != -1;
}

std::list<int> BFS::getPath(int end_node) {
    std::list<int> nodes;
    
    if (hasPath(end_node)) {
        while (end_node != start_node) {
            nodes.push_front(end_node);
            end_node = getParent(end_node);
        }
        
        nodes.push_front(start_node);
    }
    return nodes;
}