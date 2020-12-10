using System.Collections.Generic;

namespace SeminarManager.Model
{
    public class MemoryRepository : IRepository
    {
        private MemoryPersonRepository person_repository;
        private MemorySeminarRepository seminar_repository;
        private MemoryAttendeeRepository attendee_repository;

        public MemoryRepository() 
        {
            person_repository = new MemoryPersonRepository();
            seminar_repository = new MemorySeminarRepository();
            attendee_repository = new MemoryAttendeeRepository();

            CreateDummyData();
        }

        private void CreateDummyData() 
        {
            var p1 = new Person() { 
                Firstname = "Alex", 
                Lastname = "Stuckenholz", 
                IsAdmin = true, 
                EMail = "alexander.stuckenholz@hshl.de", 
                Password = "test" };

            var p2 = new Person() { 
                Firstname = "Anne", 
                Lastname = "Meier", 
                IsAdmin = false };

            person_repository.Save(p1);
            person_repository.Save(p2);

            var s1 = new Seminar() { 
                Name = "Objectoriented programming", 
                Extent = "2L2E",
                TeacherID = p1.ID };

            var s2 = new Seminar() { 
                Name = "Energy informatics", 
                Extent = "2L",
                TeacherID = p1.ID };

            seminar_repository.Save(s1);
            seminar_repository.Save(s2);

            attendee_repository.Save(s1.ID, new List<int>() { p2.ID });
            attendee_repository.Save(s2.ID, new List<int>() { p2.ID });
        }

        public IPersonRepository Persons 
        {
            get { return person_repository; }
        }

        public ISeminarRepository Seminars
        {
            get { return seminar_repository; }
        }

        public IAttendeeRepository Attendees
        {
            get { return attendee_repository; }
        }
    }
}