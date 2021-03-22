using System;
using System.IO;

namespace OOP
{
    class CsvReader
    {
        public static Order readFile(string dateiname)
        {
            var result = new Order();
            var reader = new StreamReader(dateiname);
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var elements = reader.ReadLine().Split(';');
                var order = new OrderPosition();
                order.Product = elements[0];
                order.Price = Convert.ToDouble(elements[1]);
                order.Quanttity = Convert.ToInt32(elements[2]);
                result.Add(order);
            }

            reader.Close();
            return result;
        }
    }
}