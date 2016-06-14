#include "MapTest.h"
#include "Map.h"

void MapTest::getValue_test() {
    Map m(5);
    m.insert("a", 1);
    assertEqual(m.getValue("a"), 1, "Value of key should be 1!");

    m.insert("b", 99);
    assertEqual(m.getValue("b"), 99, "Value of key should be 1!");

    std::cout << "SUCCESS: execution of Map::hasKey succesfull!" << std::endl;
}

void MapTest::hasKey_test() {
    Map m(10);
    assertFalse(m.hasKey("a"), "Map should not contain the key!");
    
    m.insert("a", 1);
    assertTrue(m.hasKey("a"), "Map should contain the key!");
    
    std::cout << "SUCCESS: execution of Map::hasKey succesfull!" << std::endl;
}

void MapTest::insert_test() {
    Map m(50);
    m.insert("a", 1);
    assertTrue(m.hasKey("a"), "Map should contain the key!");
    assertEqual(m.getValue("a"), 1, "Value of key should be 1!");
    
    std::cout << "SUCCESS: execution of Map::insert succesfull!" << std::endl;
}

void MapTest::remove_test() {
    Map m(25);
    assertFalse(m.hasKey("a"), "Map should not contain the key!");
    m.remove("a");
    assertFalse(m.hasKey("a"), "Map should not contain the key!");
    
    m.insert("a", 1);
    m.remove("a");
    assertFalse(m.hasKey("a"), "Map should not contain the key!");
    
    std::cout << "SUCCESS: execution of Map::remove succesfull!" << std::endl;
}

void MapTest::runAll() {
    std::cout << "Executing Tests for class Map!" << std::endl;
    
    hasKey_test();
    insert_test();
    remove_test();
    getValue_test();
    
    std::cout << "Execution of Tests for class Map done!" << std::endl << std::endl;
}