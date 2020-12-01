using System.Collections.Generic;
using System.Linq;
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
             return context.Seminars.Skip(from).Take(max).ToList();
        }

        public Seminar ById(int id)
        {
            return (from obj in context.Seminars where obj.ID == id select obj).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var obj = new Person() { ID =id };
            context.Persons.Attach(obj);
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
        }

        public void Save(Seminar obj)
        {
            context.Add(obj);
            context.SaveChanges();
        }
    }
}