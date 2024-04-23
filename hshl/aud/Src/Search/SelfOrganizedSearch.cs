using AUD.List;

namespace AUD.Search
{
    public class SelfOrganizedSearch : ArrayList
    {
        public SelfOrganizedSearch(int[] data) : base(data)
        {
        }

        public SelfOrganizedSearch(int count) : base(count)
        {
        }

        public bool Contains(int search_value)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == search_value)
                {
                    Swap(0, i);
                    return true;
                }
            }

            return false;
        }
    }
}
