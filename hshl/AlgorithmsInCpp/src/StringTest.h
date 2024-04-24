#ifndef STRINGTEST_H
#define	STRINGTEST_H

#include "UnitTest.h"

class StringTest : public UnitTest {
public:
    void lenght_test();
    void equals_test();
    void toLower_test();
    void toUpper_test();
    void concat_test();
    void find_test();
    void runAll();
};

#endif