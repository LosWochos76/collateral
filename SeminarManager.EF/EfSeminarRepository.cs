using System.Collections.Generic;
using SeminarManager.Model;

namespace SeminarManager.EF
{
    public class EfSeminarRepository : ISeminarRepository
    {
        private SeminarManagerContext context;

        public EfSeminarRepository(SeminarManagerContext context)
        {
            this.context = context;
        }

        public List<Seminar> All(int from = 0, int max = 1000)
        {
            throw new System.NotImplementedException();
        }

        public Seminar ById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Save(Seminar obj)
        {
            throw new System.NotImplementedException();
        }
    }
}