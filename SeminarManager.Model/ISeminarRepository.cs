using System.Collections.Generic;

namespace SeminarManager.Model
{
    public interface ISeminarRepository
    {
        List<Seminar> All(int from = 0, int max = 1000);
        Seminar ById(int id);
        void Save(Seminar obj);
        void Delete(int id);
    }
}