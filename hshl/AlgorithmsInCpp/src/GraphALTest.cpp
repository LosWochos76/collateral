#include "GraphALTest.h"
#include <iostream>
#include "GraphAL.h"

void GraphALTest::addEdge_test() {
    GraphAL g(10);
    assertFalse(g.hasEdge(1, 2), "Graph should not have an edge from 1 to 2!");
    g.addEdge(1, 2);
    assertTrue(g.hasEdge(1, 2), "Graph should have an edge from 1 to 2!");
    
    std::cout << "SUCCESS: execution of GraphAL::addEdge succesfull!" << std::endl;
}

void GraphALTest::deleteEdge_test() {

}

void GraphALTest::hasEdge_test() {

}

void GraphALTest::nodeCount_test() {
    GraphAL g1(50);
    assertEqual(g1.nodeCount(), 50, "Node count should be 50!");
    
    GraphAL g2(12);
    assertEqual(g2.nodeCount(), 12, "Node count should be 12!");
    
    std::cout << "SUCCESS: execution of GraphAL::nodeCount succesfull!" << std::endl;
}

void GraphALTest::nodeFrom_test() {

}

void GraphALTest::runAll() {
    std::cout << "Executing Tests for class GraphAL!" << std::endl;
    
    nodeCount_test();
    hasEdge_test();
    addEdge_test();
    deleteEdge_test();
    nodeFrom_test();
    
    std::cout << "Execution of Tests for class GraphAL done!" << std::endl << std::endl;
}