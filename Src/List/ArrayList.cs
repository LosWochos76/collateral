using System;

namespace AUD.List
{
	public class ArrayList
	{
        protected int[] data;

        public ArrayList(int count)
        {
            data = new int[count];
        }

        public ArrayList(int[] data)
        {
            this.data = data;
        }

        public int[] Data { get { return data; } }

		public void Swap(int pos1, int pos2)
		{
			if (pos1 == pos2)
				return;

			int tmp = data[pos1];
            data[pos1] = data[pos2];
            data[pos2] = tmp;
		}

        public void FillWithRandomNumbers(int max_value)
        {
            Random rnd = new Random();
            for (int i = 0; i < data.Length; i++)
                data[i] = rnd.Next(max_value);
        }

		public override string ToString()
		{
			return "[" + String.Join(", ", data) + "]";
		}

		public void Print()
		{
			Console.WriteLine(ToString());
		}

        public bool IsSorted()
        {
            for (int i = 1; i < data.Length; i++)
                if (data[i] < data[i - 1])
                    return false;

            return true;
        }

        public bool ContainsLinear(int search_value)
        {
            for (int i = 0; i < data.Length; i++)
                if (data[i] == search_value)
                    return true;

            return false;
        }

        public bool ContainsBinary(int search_value)
        {
            return ContainsBinaryRecursive(0, data.Length, search_value);
        }

        private bool ContainsBinaryRecursive(int from, int to, int search_value)
        {
            if (to <= from)
                return false;

            int middle_pos = (from + to) / 2;

            if (data[middle_pos] == search_value)
                return true;
            else if (search_value < data[middle_pos])
                return ContainsBinaryRecursive(0, middle_pos, search_value);
            else
                return ContainsBinaryRecursive(middle_pos + 1, to, search_value);
        }
    }
}