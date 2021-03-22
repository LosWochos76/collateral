using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP
{
    class Order
    {
        private List<OrderPosition> _list = new List<OrderPosition>();
        private double _gesamtumsatz = 0;

        public void Add(OrderPosition o)
        {
            _list.Add(o);
            _gesamtumsatz += o.Price * o.Quanttity;
        }

        public double Gesamtumsatz
        {
            get { return _gesamtumsatz; }
        }
    }
}
