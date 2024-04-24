#include <cstdlib>
#include <iostream>
#include "StringTest.h"
#include "ArrayListTest.h"
#include "MapTest.h"
#include "GraphALTest.h"

int main() {
    StringTest st;
    st.runAll();
    
    ArrayListTest at;
    at.runAll();
    
    MapTest mt;
    mt.runAll();
    
    GraphALTest galt;
    galt.runAll();
    
    return 0;
}