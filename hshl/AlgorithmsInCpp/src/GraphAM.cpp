#include "GraphAM.h"
#include <stdio.h>

GraphAM::GraphAM(int count) {
    node_count = count;
    this->adjacency_matrix = new int[count*count];
    for (int i=0; i<count*count; i++)
        this->adjacency_matrix[i] = 0;
}

int GraphAM::nodeCount() {
    return node_count;
}

void GraphAM::addEdge(int u, int v) {
    adjacency_matrix[(u-1)*node_count+v-1] = 1;
}

void GraphAM::deleteEdge(int u, int v) {
    adjacency_matrix[(u-1)*node_count+v-1] = 0;
}

bool GraphAM::hasEdge(int u, int v) {
    return this->adjacency_matrix[(u-1)*node_count+v-1] == 1;
}

void GraphAM::print() {
    for (int i=0; i<node_count; i++) {
        for (int j=0; j<node_count; j++) {
            printf("%i ", this->adjacency_matrix[i*node_count+j]);
        }
        printf("\n");
    }
}

std::list<int> GraphAM::nodesFrom(int node) {
    std::list<int> nodes;
    for (int i=1; i<=node_count; i++)
        if (hasEdge(node, i))
            nodes.push_back(i);
    
    return nodes;
}
