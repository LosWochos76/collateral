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

        public List<int> Get(Seminar seminar)
        {
            return (from a in context.Attendees 
                where a.SeminarID == seminar.ID select a.PersonID).ToList();
        }

        public void Save(Seminar seminar, List<int> attendees)
        {
            var elements = (from a in context.Attendees where a.SeminarID == seminar.ID select a);
            context.RemoveRange(elements);

            foreach (int person_id in attendees)
            {
                var a = new Attendee() { SeminarID=seminar.ID, PersonID=person_id };
                context.Attendees.Add(a);
            }

            context.SaveChanges();
        }
    }
}