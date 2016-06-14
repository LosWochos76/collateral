#ifndef MAPTEST_H
#define	MAPTEST_H

#include "UnitTest.h"

class MapTest : public UnitTest {
public:
    void insert_test();
    void remove_test();
    void hasKey_test();
    void getValue_test();
    void runAll();

private:

};

#endif