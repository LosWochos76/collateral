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