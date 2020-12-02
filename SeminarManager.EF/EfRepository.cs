using System;
using SeminarManager.Model;
using Microsoft.EntityFrameworkCore;

namespace SeminarManager.EF
{
    public class EfRepository : IRepository
    {
        private EfPersonRepository person_repository;
        private EfSeminarRepository seminar_repository;
        private SeminarManagerContext context;

        public EfRepository()
        {
            context = new SeminarManagerContext();
            context.Database.Migrate();

            person_repository = new EfPersonRepository(context);
            seminar_repository = new EfSeminarRepository(context);

            if (person_repository.All().Count == 0)
            {
                person_repository.Save(new Person()
                {
                    Firstname = "Alexander",
                    Lastname = "Stuckenholz",
                    EMail = "alexander.stuckenholz@hshl.de",
                    IsAdmin = true,
                    Password = "test"
                });
            }
        }

        private string GetFromEnvironmentOrDefault(string key, string def)
        {
            string var = Environment.GetEnvironmentVariable(key);
            if (var == null || var == string.Empty)
                return def;
            else
                return var;
        }

        public IPersonRepository Persons
        {
            get { return person_repository; }
        }

        public ISeminarRepository Seminars
        {
            get { return seminar_repository; }
        }
    }
}