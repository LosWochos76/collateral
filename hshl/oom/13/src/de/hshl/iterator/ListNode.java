package de.hshl.iterator;

public class ListNode<T> {
   private T value;
   private ListNode next;

   public T getValue() {
      return value;
   }

   public void setValue(T value) {
      this.value = value;
   }

   public ListNode getNext() {
      return next;
   }

   public void setNext(ListNode next) {
      this.next = next;
   }

   public ListNode(T value) {
      setValue(value);
   }

   public ListNode(T value, ListNode next) {
      this(value);
      setNext(next);
   }
}
