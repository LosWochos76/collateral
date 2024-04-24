#ifndef GRAPHALTEST_H
#define	GRAPHALTEST_H

#include "UnitTest.h"

class GraphALTest : public UnitTest {
public:
    void runAll();
    void nodeCount_test();
    void addEdge_test();
    void deleteEdge_test();
    void hasEdge_test();
    void nodeFrom_test();
};

#endif