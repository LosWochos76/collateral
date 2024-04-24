#include "Graph.h"
#include <stdlib.h>
#include <time.h>
#include <iostream>
#include <fstream>

void Graph::randomEdges(int edge_count) {
    srand(time(NULL));
    int node_count = this->nodeCount();
    
    for (int i=0; i<edge_count; i++)
        this->addEdge(rand() % node_count + 1, rand() % node_count + 1);
}

std::string Graph::toDot() {
    std::string result = "digraph graphAL {\n";
    
    for (int i=1; i<=this->nodeCount(); i++) {
        std::list<int> edges = this->nodesFrom(i);
        while (!edges.empty()) {
            result += std::to_string(i) + " -> " + std::to_string(edges.front()) + ";\n";
            edges.pop_front();
        }
    }
    
    result += "}";
    return result;
}

void Graph::toDotFile(const char* filename) {
    std::ofstream dotFile;
    dotFile.open(filename);
    dotFile << this->toDot();
    dotFile.close();
}