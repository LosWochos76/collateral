using System.Collections.Generic;
using SeminarManager.Model;
using System.Linq;
using SimpleHashing.Net;
using Microsoft.EntityFrameworkCore;

namespace SeminarManager.EF
{
    public class EfPersonRepository : IPersonRepository
    {
        private ISimpleHash simpleHash = new SimpleHash();
        private SeminarManagerContext context;

        public EfPersonRepository(SeminarManagerContext context)
        {
            this.context = context;
        }

        public List<Person> All(int from = 0, int max = 1000)
        {
            return context.Persons
                .OrderBy(p => p.Lastname).ThenByDescending(p => p.Firstname)
                .AsNoTracking().Skip(from).Take(max).ToList();
        }

        public Person ById(int id)
        {
            return (from obj in context.Persons where obj.ID == id select obj)
                .AsNoTracking().FirstOrDefault();
        }

        public void Delete(int id)
        {
            var obj = context.Persons.Find(id);
            if (obj != null) 
            {
                context.Persons.Remove(obj);
                context.SaveChanges();
            }
        }

        public Person FindAdminByEmailAndPassword(string email, string password)
        {
            var list = (from obj in context.Persons 
                where obj.EMail.Equals(email) select obj).AsNoTracking().ToList();

            foreach (var obj in list)
                if (simpleHash.Verify(password, obj.Password))
                    return obj;
            
            return null;
        }

        public void Save(Person obj)
        {
            obj.Password = obj.IsAdmin ? simpleHash.Compute(obj.Password) : string.Empty;

            if (obj.ID == 0) 
                context.Add(obj);
            else
                context.Persons.Attach(obj).State = EntityState.Modified;

            context.SaveChanges();
            context.Entry(obj).State = EntityState.Detached;
        }
    }
}