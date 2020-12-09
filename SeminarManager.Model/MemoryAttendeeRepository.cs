using System.Collections.Generic;
using System.Linq;

namespace SeminarManager.Model
{
    public class MemoryAttendeeRepository : IAttendeeRepository
    {
        private List<Attendee> objects = new List<Attendee>();

        public List<int> Get(Seminar seminar)
        {
            return (from obj in objects where obj.SeminarID == seminar.ID select obj.PersonID).ToList();
        }

        public void Save(Seminar seminar, List<int> attendees)
        {
            objects.RemoveAll(obj => obj.SeminarID == seminar.ID);
            foreach (int id in attendees) 
                objects.Add(new Attendee() { SeminarID=seminar.ID, PersonID=id });
        }
    }
}