package de.hshl.iterator;

import java.util.Iterator;

public class LinkedListIterator<T> implements Iterator<T> {
   private LinkedList<T> list;
   private ListNode<T> current;

   public LinkedListIterator(LinkedList<T> list) {
      this.list = list;
      this.current = list.getHead();
   }

   @Override
   public boolean hasNext() {
      return current != null;
   }

   @Override
   public T next() {
      T value = current.getValue();
      current = current.getNext();
      return value;
   }
}
