#include "DFS.h"
#include <iostream>

DFS::DFS(Graph *g) {
    this->g = g;
    this->has_cycle = false;
    this->components = 0;
}

void DFS::explore() {
    for (int i=1; i<=g->nodeCount(); i++) {
        color[i] = WHITE;
        parent[i] = -1;
        component[i] = 0;
    }
    
    for (int i=1; i<=g->nodeCount(); i++) {
        if (component[i] == 0)
            components++;
        
        if (color[i] == WHITE)
            visit(i);
    }
}

void DFS::visit(int node) {
    color[node] = GRAY;
    component[node] = this->components;
    
    std::list<int> adj = g->nodesFrom(node);
    while (!adj.empty()) {
        int v = adj.front();
        adj.pop_front();
        if (color[v] == WHITE) {
            parent[v] = node;
            visit(v);
        } else if (color[v] == GRAY) {
            has_cycle = true;
        }
    }
    
    color[node] = BLACK;
    topolic.push_back(node);
}

int DFS::getColor(int node) {
    return color[node];
}

int DFS::getParent(int node) {
    return parent[node];
}

bool DFS::isCyclic() {
    return this->has_cycle;
}

int DFS::getNoOfComponents() {
    return this->components;
}

std::list<int> DFS::getTopologicOrder() {
    return this->topolic;            
}
