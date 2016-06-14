#ifndef MAP_H
#define	MAP_H

#include "String.h"

class Map {
private:
    struct tuple {
        const char *key;
        int value;
        tuple* next;
    };
    
    int size;
    typedef tuple* tuplePtr;
    tuplePtr* array;
    static int compute_hash(const char *key, int size);
    tuplePtr getElement(const char *key) const;

public:
    Map(int size);
    ~Map();
    void insert(const char *key, int value);
    void remove(const char *key);
    bool hasKey(const char *key) const;
    int getValue(const char *key) const;
};

#endif