using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeminarManager.Model
{
    public interface ISeminarRepository
    {
        int Count { get; }
        List<Seminar> All(int from = 0, int max = 1000);
        Seminar ById(int id);
        void Save(Seminar obj);
        void Delete(int id);
    }
}