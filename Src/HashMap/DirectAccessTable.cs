namespace AUD.HashMap
{
    public class DirectAccessTable
    {
        private Tuple[] items;

        public DirectAccessTable(int size)
        {
            items = new Tuple[size];
        }

        public void Insert(int key, string value)
        {
            items[key] = new Tuple(key, value);
        }

        public bool ContainsKey(int key)
        {
            return items[key] != null;
        }

        public string GetValue(int key)
        {
            return items[key].Value;
        }
    }
}