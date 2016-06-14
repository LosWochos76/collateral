#include "ArrayListTest.h"
#include "ArrayList.h"
#include <iostream>

void ArrayListTest::count_test() {
    ArrayList a;
    assertEqual(0, a.count(), "ArrayList should not contain any elements!");
    
    a.add(1);
    assertEqual(1, a.count(), "ArrayList should contain 1 elements!");
    
    std::cout << "SUCCESS: execution of ArrayList::count succesfull!" << std::endl;
}

void ArrayListTest::at_test() {
    ArrayList a;
    a.add(1);
    assertEqual(1, a[0], "ArrayList should contain 1 at pos 0!");
    
    a.add(5);
    assertEqual(5, a[1], "ArrayList should contain 5 at pos 1!");
    
    std::cout << "SUCCESS: execution of ArrayList::at succesfull!" << std::endl;
}

void ArrayListTest::add_test() {
    ArrayList a;
    for (int i=0; i<15; i++)
        a.add(i);
    
    assertEqual(15, a.count(), "ArrayList should contain 15 elements!");
    assertEqual(14, a[14], "ArrayList should contain 14 at pos 14!");
    
    std::cout << "SUCCESS: execution of ArrayList::add succesfull!" << std::endl;
}

void ArrayListTest::runAll() {
    std::cout << "Executing Tests for class ArrayList!" << std::endl;
    count_test();
    at_test();
    add_test();
    std::cout << "Execution of Tests for class ArrayList done!" << std::endl << std::endl;
}