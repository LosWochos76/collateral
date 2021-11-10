namespace AUD.List
{
    public class LinkedList
    {
        private ListNode head = null;
        private ListNode tail = null;
        private int count = 0;

        public int Count
        {
            get { return count; }
        }

        public void PushFront(int value)
        {
            var element = new ListNode(value);
            
            if (head == null)
            {
                head = element;
                tail = element;
                count = 1;
            }
            else
            {
                element.Next = head;
                head = element;
                count++;
            }
        }

        public void PushBack(int value)
        {
            var element = new ListNode(value);

            if (tail == null)
            {
                head = element;
                tail = element;
                count = 1;
            }
            else
            {
                tail.Next = element;
                tail = element;
                count++;
            }
        }

		public int PopFront()
		{
			// Hier fehlt ein wenig Programmcode.
			return 0;
		}

		public int PopBack()
		{
			// Hier fehlt ein wenig Programmcode.
			return 0;
		}

        public int[] toArray()
        {
            int[] result = new int[count];

            /* Hier muss nun jedes Element der verketteten Liste in 
               in das Array result eingefügt werden. */

            return result;
        }

        public override string ToString()
        {
            var data = toArray();
            var list = new ArrayList(data);
            return data.ToString();
        }

        public bool Contains(int value)
        {
            ListNode node = head;
            while (node != null)
            {
                if (node.Value == value)
                    return true;

                node = node.Next;
            }

            return false;
        }

        private ListNode FindNodeToInsert(int value)
        {
            ListNode currentElement = head.Next;
            ListNode lastElement = head;

            do
            {
                if (currentElement.Value > value)
                    return lastElement;

                lastElement = currentElement;
                currentElement = currentElement.Next;
            } while (currentElement != null);

            return null;
        }

        public void InsertSorted(int value)
        {
	        if (count == 0)
		        PushFront(value);
	        else if (head.Value > value)
		        PushFront(value);
	        else if (tail.Value <= value)
		        PushBack(value);
	        else {
                var elementToInsertAfter = FindNodeToInsert(value);
                var newElement = new ListNode(value);
                newElement.Next = elementToInsertAfter.Next;
                elementToInsertAfter.Next = newElement;
	        }
        }

		public bool IsSorted
        {
            get
            {
                if (count < 2)
                    return true;

                ListNode last = head;
                ListNode current = head.Next;

                while (current != null)
                {
                    if (current.Value < last.Value)
                        return false;

                    last = current;
                    current = current.Next;
                }

                return true;
            }
        }
    }
}