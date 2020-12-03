using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
             return context.Seminars.AsNoTracking().Skip(from).Take(max).ToList();
        }

        public Seminar ById(int id)
        {
            return (from obj in context.Seminars where obj.ID == id select obj)
                .AsNoTracking().FirstOrDefault();
        }

        public void Delete(int id)
        {
            var obj = context.Seminars.Find(id);
            if (obj != null) 
            {
                context.Seminars.Remove(obj);
                context.SaveChanges();
            }
        }

        public void Save(Seminar obj)
        {
            if (obj.ID == 0) 
            {
                context.Add(obj);
                context.SaveChanges();
            }
            else
            {
                context.Seminars.Attach(obj).State = EntityState.Modified;
                context.SaveChanges();
                context.Entry(obj).State = EntityState.Detached;
            }
        }
    }
}