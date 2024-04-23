using System.Collections.Generic;
using SeminarManager.Model;
using System.Linq;

namespace SeminarManager.EF
{
    public class EfAttendeeRepository : IAttendeeRepository
    {
        private SeminarManagerContext context;

        public EfAttendeeRepository(SeminarManagerContext context)
        {
            this.context = context;
        }

        public List<int> Get(int seminar_id)
        {
            return (from a in context.Attendees 
                where a.SeminarID == seminar_id select a.PersonID).ToList();
        }

        public void Save(int seminar_id, List<int> attendees)
        {
            var elements = (from a in context.Attendees where a.SeminarID == seminar_id select a);
            context.RemoveRange(elements);

            foreach (int person_id in attendees)
            {
                var a = new Attendee() { SeminarID = seminar_id, PersonID = person_id };
                context.Attendees.Add(a);
            }

            context.SaveChanges();
        }
    }
}