package de.hshl;

public class ArrayList {
   private int data[];
   private int current_index = 0;
   
   public ArrayList() {
      this.data = new int[10];
   }
   
   public ArrayList(int initial_count) {
      this.data = new int[initial_count];
   }
   
   public int get(int index) {
      if (index > current_index)
         throw new RuntimeException("Index beyond boundary!");

      return this.data[index];
   }
   
   public void set(int index, int value) {
      if (index > current_index)
         throw new RuntimeException("Index beyond boundary!");
      
      this.data[index] = value;
   }
   
   public int size() {
      return current_index;
   }
   
   public void add(int value) {
      resize(current_index);
      set(current_index, value);
      current_index++;
   }
   
   private void resize(int index_to_fit) {
      if (index_to_fit < data.length)
         return;
      
      int new_size = data.length;
      while (new_size <= index_to_fit)
         new_size *= 2;
      
      int[] new_data = new int[new_size];
      System.arraycopy(data, 0, new_data, 0, data.length);
      this.data = new_data;
   }
}