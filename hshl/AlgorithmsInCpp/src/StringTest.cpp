#include "StringTest.h"
#include "String.h"

void StringTest::lenght_test() {
    String s1("");
    assertEqual(0, s1.length(), "Lenght of string should be 0!");
    
    String s2("Hallo");
    assertEqual(5, s2.length(), "Lenght of string should be 5!");
    
    std::cout << "SUCCESS: execution of String::length succesfull!" << std::endl;
}

void StringTest::toLower_test() {
    String s1("HALLO");
    String s2("hallo");
    String s = s1.toLower();
    assertTrue(s.equals(s2), "String should be in lower case after toLower!");
    
    std::cout << "SUCCESS: execution of String::toLower succesfull!" << std::endl;
}

void StringTest::toUpper_test() {
    String s1("hallo");
    String s2("HALLO");
    String s = s1.toUpper();
    assertTrue(s.equals(s2), "String should be in upper case after toLower!");
    
    std::cout << "SUCCESS: execution of String::toUpper succesfull!" << std::endl;
}

void StringTest::equals_test() {
    String a1("");
    String a2("");
    assertTrue(a1.equals(a2), "Strings should be equal!");
    
    String b1("HALLO");
    String b2("HALLO");
    assertTrue(b1.equals(b2), "Strings should be equal!");
    
    String c1("hallo");
    String c2("HALLO");
    assertFalse(c1.equals(c2), "Strings should not be equal!");
    
    std::cout << "SUCCESS: execution of String::equals succesfull!" << std::endl;
}

void StringTest::concat_test() {
    String s1("Hallo ");
    String s2("Welt");
    String s3 = s1 + s2;
    String s4("Hallo Welt");
    assertTrue(s3.equals(s4), "Strings should be equal!");
    
    std::cout << "SUCCESS: execution of String::concat succesfull!" << std::endl;
}

void StringTest::find_test() {
    String s1("Hallo");
    assertEqual(0, s1.find("Ha"), "Find should deliver position 0!");
    
    String s2("");
    assertEqual(-1, s2.find("Ha"), "Find should deliver position -1!");

    String s3("Hallo");
    assertEqual(4, s3.find("o"), "Find should deliver position 4!");
    
    std::cout << "SUCCESS: execution of String::find succesfull!" << std::endl;
}

void StringTest::runAll() {
    std::cout << "Executing Tests for class String!" << std::endl;
    
    lenght_test();
    equals_test();
    toLower_test();
    toUpper_test();
    concat_test();
    find_test();
    
    std::cout << "Execution of Tests for class String done!" << std::endl << std::endl;
}