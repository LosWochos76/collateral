using System.Collections.Generic;
using System.Linq;

namespace SeminarManager.Model
{
    public class MemoryAttendeeRepository : IAttendeeRepository
    {
        private List<Attendee> objects = new List<Attendee>();

        public List<int> Get(int seminar_id)
        {
            return (from obj in objects where obj.SeminarID == seminar_id select obj.PersonID).ToList();
        }

        public void Save(int seminar_id, List<int> attendees)
        {
            objects.RemoveAll(obj => obj.SeminarID == seminar_id);
            foreach (int id in attendees) 
                objects.Add(new Attendee() { SeminarID = seminar_id, PersonID=id });
        }
    }
}