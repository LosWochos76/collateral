namespace SeminarManager.Model
{
    public class MemoryRepository : IRepository
    {
        private MemoryPersonRepository person_repository;
        private MemorySeminarRepository seminar_repository;

        public MemoryRepository() 
        {
            person_repository = new MemoryPersonRepository();
            seminar_repository = new MemorySeminarRepository();

            person_repository.Save(new Person() { 
                Firstname = "Alex", 
                Lastname = "Stuckenholz", 
                IsAdmin = true, 
                EMail = "alexander.stuckenholz@hshl.de", 
                Password = "test" });

            person_repository.Save(new Person() { 
                Firstname = "Anne", 
                Lastname = "Meier", 
                IsAdmin = false });

            seminar_repository.Save(new Seminar() { 
                Name = "Objectoriented programming", 
                Extent = "2L2E" });

            seminar_repository.Save(new Seminar() { 
                Name = "Energy informatics", 
                Extent = "2L" });
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