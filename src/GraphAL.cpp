#include "GraphAL.h"
#include <iostream>
#include <algorithm>

GraphAL::GraphAL(int count) {
    node_count = count;
    adjacency_list = new std::list<int>[node_count];
}

int GraphAL::nodeCount() {
    return node_count;
}

void GraphAL::addEdge(int u, int v) {
    adjacency_list[u-1].push_back(v);
}

void GraphAL::deleteEdge(int u, int v) {
    adjacency_list[u-1].remove(v);
}

bool GraphAL::hasEdge(int u, int v) {
    std::list<int>::iterator i = find(adjacency_list[u-1].begin(), adjacency_list[u-1].end(), v);
    return i != adjacency_list[u-1].end();
}

void GraphAL::print() {
    for (int i=0; i<node_count; i++) {
        std::list<int>::iterator it = adjacency_list[i].begin();
        std::cout << i+1 << "->";
        while (it != adjacency_list[i].end()) {
            std::cout << *it << "->";
            it++;
        }
        std::cout << std::endl;
    }
}

std::list<int> GraphAL::nodesFrom(int node) {
    return adjacency_list[node-1];
}
