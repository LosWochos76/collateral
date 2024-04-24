#ifndef UNITTEST_H
#define	UNITTEST_H

#include <iostream>

class UnitTest {
public:
    void assertTrue(bool a, const char* error);
    void assertFalse(bool a, const char* error);
    void assertEqual(int a, int b, const char* error);
    void assertEqual(double a, double b, const char* error);
};

#endif