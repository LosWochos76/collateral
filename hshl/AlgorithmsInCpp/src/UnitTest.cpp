#include "UnitTest.h"
#include <stdlib.h>

void UnitTest::assertTrue(bool a, const char* error) {
    if (!a) {
        std::cout << "ERROR: " << error << std::endl;
        exit(1);
    }
}

void UnitTest::assertFalse(bool a, const char* error) {
    if (a) {
        std::cout << "ERROR: " << error << std::endl;
        exit(1);
    }
}

void UnitTest::assertEqual(int a, int b, const char* error) {
    if (a != b) {
        std::cout << "ERROR: " << error << std::endl;
        exit(1);
    }
}

void UnitTest::assertEqual(double a, double b, const char* error) {
    if (a != b) {
        std::cout << "ERROR: " << error << std::endl;
        exit(1);
    }
}
