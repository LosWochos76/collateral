#ifndef ARRAYLIST_H
#define	ARRAYLIST_H

class ArrayList {
private:
    int *data;
    int table_size;
    int item_count;
    void swap(int* array, int pos1, int pos2);
    int quickSortPartition(int *array, int from, int to);
    void quickSort(int *array, int from, int to);
    
public:
    ArrayList();
    ArrayList(int *data, int count);
    ~ArrayList();
    int count();
    void add(int value);
    int at(int pos);
    int operator[](int pos);
    void sort();
};

#endif