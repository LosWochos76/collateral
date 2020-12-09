using System.Collections.Generic;

namespace SeminarManager.Model
{
    public interface IAttendeeRepository
    {
        void Save(Seminar seminar, List<int> attendees);
        List<int> Get(Seminar seminar);
    }
}