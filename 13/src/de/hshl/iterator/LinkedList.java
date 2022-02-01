package de.hshl.iterator;

import java.util.Iterator;

public class LinkedList<T> implements Iterable {
   private ListNode<T> head;

   public ListNode<T> getHead() {
      return head;
   }

   public void push(T value) {
      var node = new ListNode(value);
      node.setNext(head);
      head = node;
   }

   public T pop() throws Exception {
      if (head == null)
         throw new Exception("List is empty!");

      T value = head.getValue();
      head = head.getNext();
      return value;
   }

   @Override
   public Iterator<T> iterator() {
      return new LinkedListIterator(this);
   }
}
