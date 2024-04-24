#include "Map.h"
#include <iostream>
#include <exception>

using namespace std;

Map::Map(int size) {
    this->size = size;
    this->array = new tuplePtr[size];
    for (int i=0; i<size; i++) {
        this->array[i] = NULL;
    }
}

Map::~Map() {
    for (int i=0; i<size; i++) {
        tuplePtr t = this->array[i];
        while (t != NULL) {
            tuplePtr next = t->next;
            delete t;
            t = next;
        }
    }
}

int Map::compute_hash(const char *key, int size) {
    int a = 31415;
    int b = 27183;
    int pos = 0;
    int h = 0;
    
    while (key[pos] != '\0') {
        h = (a*h + key[pos]) % size;
        pos++;
        a = a*b % (size-1);
    }
    
    return h < 0 ? h + size : h;
}

Map::tuplePtr Map::getElement(const char *key) const {
    int pos = Map::compute_hash(key, size);
    
    tuplePtr t = this->array[pos];
    while (t != NULL) {
        if (t->key == key)
            return t;
        t = t->next;
    }
    
    return NULL;
}

bool Map::hasKey(const char *key) const {
    return getElement(key) != NULL;
}

void Map::insert(const char *key, int value) {
    int pos = Map::compute_hash(key, size);
    tuple *t = new tuple;
    t->key = key;
    t->value = value;
    t->next = NULL;
    
    if (this->array[pos] == NULL)
        this->array[pos] = t;
    else {
        t->next = this->array[pos];
        this->array[pos] = t;
    }
}

void Map::remove(const char *key) {
    int pos = Map::compute_hash(key, size);
    
    tuplePtr t = this->array[pos];
    tuplePtr last = NULL;
    
    while (t != NULL) {
        if (t->key == key) {
            if (last == NULL) {
                this->array[pos] = t->next;
            } else {
                last = t->next;
                delete t;
            }
            
            return;
        }
        
        last = t;
        t = t->next;
    }
}

int Map::getValue(const char* key) const {
    tuplePtr t = getElement(key);
    return t->value;
}