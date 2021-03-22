using System;

namespace OOP
{
    class Logging
    {
        private static Logging instance = null;

        private Logging()
        {
        }

        public static Logging getInstance()
        {
            if (instance == null)
                instance = new Logging();

            return instance;
        }

        public void debug(string message)
        {
            Console.WriteLine(
               DateTime.Now.ToString("dd.MM.yyyy - hh:mm:ss:") + " DEBUG: " + message);
        }
    }
}
