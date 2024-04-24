#include "ArrayList.h"
#include "assert.h"
#include <iostream>

ArrayList::ArrayList() {
    this->table_size = 10;
    this->data = new int[table_size];
    this->item_count = 0;
}

ArrayList::ArrayList(int* data, int count) {
    this->table_size = count * 2;
    this->data = new int[table_size];
    this->item_count = count;
    for (int i=0; i<count; i++)
        this->data[i] = data[i];
}

ArrayList::~ArrayList() {
    delete[] data;
}

int ArrayList::at(int pos) {
    assert(pos <= item_count);
    return data[pos];
}

int ArrayList::operator[](int pos) {
    return at(pos);
}

int ArrayList::count() {
    return item_count;
}

void ArrayList::add(int value) {
    if (item_count+1 > table_size) {
        int *new_data = new int[table_size*2];
        for (int i=0; i<table_size; i++)
            new_data[i] = data[i];
        
        table_size *= 2;
        delete[] data;
        data = new_data;
    }
    
    data[this->item_count] = value;
    this->item_count++;
}

void ArrayList::swap(int* array, int pos1, int pos2) {
    if (pos1 == pos2)
        return;
    
    int tmp = array[pos1];
    array[pos1] = array[pos2];
    array[pos2] = tmp;
}

int ArrayList::quickSortPartition(int *array, int from, int to) {
    int pivot = array[to];
    int left = from;
    int right = to;
    
    do {
        while (right > from && array[right] >= pivot)
            right--;

        while (array[left] < pivot)
            left++;
        
        if (left < right)
            swap(array, left, right);
    } while (left < right);
    
    swap(array, left, to);
    return left;
}

void ArrayList::quickSort(int *array, int from, int to) {
    if (from < to) {
        int pivotIndex = quickSortPartition(array, from, to);
        quickSort(array, from, pivotIndex-1);
        quickSort(array, pivotIndex+1, to);
    }
}

void ArrayList::sort() {
    quickSort(data, 0, item_count-1);
}